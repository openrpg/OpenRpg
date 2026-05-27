using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Combat.Processors.Attacks;
using OpenRpg.Combat.Processors.Attacks.Entity;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Requirements;
using OpenRpg.Data;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Types;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Types;
using OpenRpg.Items.Templates;
using OpenRpg.Genres.Requirements;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Combat;

public class TurnManager
{
    private static readonly Random _rng = new();
    private readonly IDataSource _dataSource;
    private readonly ICharacterRequirementChecker _requirementChecker;
    private readonly ILocaleDataSource _localeDataSource;
    private readonly IEntityAttackGenerator _attackGenerator;
    private readonly IEntityAttackProcessor _attackProcessor;
    private List<BattleEntity> _party;
    private List<BattleEntity> _enemies;
    private double _phaseTimer;

    public enum Phase { Idle, TurnDwell, TargetFlash, GameOver, PlayerInput }
    public Phase CurrentPhase { get; private set; } = Phase.Idle;
    public BattleEntity CurrentAttacker { get; private set; }
    public IReadOnlyList<BattleEntity> CurrentTargets { get; private set; } = [];
    public Team WinningTeam { get; private set; }
    public int CurrentTurnIndex { get; private set; } = -1;
    public string LastActionMessage { get; private set; } = "";
    public List<BattleEntity> TurnOrder { get; private set; } = [];
    public PlayerAction PendingAction { get; set; }
    public bool IsInPlayerInput => CurrentPhase == Phase.PlayerInput;
    public List<BattleEntity> HighlightedTargets { get; set; } = [];

    public event Action<BattleEntity, int, bool> OnDamageDealt;

    public TurnManager(
        IDataSource dataSource,
        ICharacterRequirementChecker requirementChecker,
        ILocaleDataSource localeDataSource,
        IEntityAttackGenerator attackGenerator,
        IEntityAttackProcessor attackProcessor)
    {
        _dataSource = dataSource;
        _requirementChecker = requirementChecker;
        _localeDataSource = localeDataSource;
        _attackGenerator = attackGenerator;
        _attackProcessor = attackProcessor;
    }

    public void Start(List<BattleEntity> party, List<BattleEntity> enemies)
    {
        _party = party;
        _enemies = enemies;
        TurnOrder = party.Concat(enemies).OrderByDescending(e => e.Initiative).ToList();
        CurrentTurnIndex = -1;
        CurrentPhase = Phase.Idle;
        _phaseTimer = 0;
        LastActionMessage = "";
    }

    public void Update(double dt)
    {
        switch (CurrentPhase)
        {
            case Phase.Idle:
                var nextPhase = AdvanceTurn();
                if (nextPhase != Phase.Idle)
                {
                    CurrentPhase = nextPhase;
                    _phaseTimer = 0;
                }
                break;

            case Phase.TurnDwell:
                _phaseTimer += dt;
                if (_phaseTimer >= 0.8)
                {
                    ExecuteAction();
                    CurrentPhase = Phase.TargetFlash;
                    _phaseTimer = 0;
                }
                break;

            case Phase.PlayerInput:
                break;

            case Phase.TargetFlash:
                _phaseTimer += dt;
                if (_phaseTimer >= 0.3)
                {
                    CurrentPhase = CheckGameOver() ? Phase.GameOver : Phase.Idle;
                    _phaseTimer = 0;
                }
                break;
        }
    }

    private Phase AdvanceTurn()
    {
        var count = TurnOrder.Count;
        for (var i = 0; i < count; i++)
        {
            CurrentTurnIndex = (CurrentTurnIndex + 1) % count;
            CurrentAttacker = TurnOrder[CurrentTurnIndex];
            if (!CurrentAttacker.IsAlive) continue;

            var targets = CurrentAttacker.Team == Team.Player ? _enemies : _party;
            if (!targets.Any(e => e.IsAlive)) continue;

            return CurrentAttacker.Team == Team.Player
                ? Phase.PlayerInput
                : Phase.TurnDwell;
        }
        return Phase.Idle;
    }

    private void ExecuteAction()
    {
        if (CurrentAttacker.Team == Team.Player && PendingAction != null)
        {
            var action = PendingAction;
            PendingAction = null;
            switch (action.Type)
            {
                case ActionType.BasicAttack:
                    ExecuteBasicAttack(action.Targets?.FirstOrDefault());
                    return;
                case ActionType.Ability:
                    if (action.Ability != null)
                    {
                        ExecuteAbility(CurrentAttacker, action.Ability, action.Targets);
                        return;
                    }
                    break;
                case ActionType.UseItem:
                    if (action.UsedItem != null && action.Targets?.Count > 0)
                    {
                        UseItemOnTarget(action.UsedItem, action.Targets[0]);
                        return;
                    }
                    break;
            }
        }

        if (CurrentAttacker.Team == Team.Player)
        {
            if (TryPickAndExecuteAbility(CurrentAttacker))
                return;
        }

        ExecuteBasicAttack();
    }

    public void SubmitPlayerAction(PlayerAction action)
    {
        if (CurrentPhase != Phase.PlayerInput) return;
        PendingAction = action;
        ExecuteAction();
        CurrentPhase = Phase.TargetFlash;
        _phaseTimer = 0;
    }

    public List<(AbilityTemplate Template, int ManaCost, bool CanAfford)> GetAvailableAbilities(BattleEntity entity)
    {
        var result = new List<(AbilityTemplate, int, bool)>();
        foreach (var (template, manaCost) in GetValidAbilities(entity))
        {
            var canAfford = entity.Mana >= manaCost;
            result.Add((template, manaCost, canAfford));
        }
        return result;
    }

    private List<(AbilityTemplate Template, int ManaCost)> GetValidAbilities(BattleEntity entity)
    {
        if (!entity.Entity.Variables.ContainsKey(CombatTemplateVariableTypes.Abilities))
            return [];

        var abilities = entity.Entity.Variables.GetAs<List<AbilityData>>(CombatTemplateVariableTypes.Abilities);
        if (abilities == null || abilities.Count == 0)
            return [];

        var valid = new List<(AbilityTemplate, int)>();

        foreach (var abilityData in abilities)
        {
            var template = _dataSource.Get<AbilityTemplate>(abilityData.TemplateId);
            if (template == null) continue;

            var requirements = template.Variables.GetAsOrDefault<IReadOnlyCollection<Requirement>>(
                CoreTemplateVariableTypes.Requirements, () => Array.Empty<Requirement>());
            if (!_requirementChecker.AreRequirementsMet(entity.Entity, requirements))
                continue;

            var manaCost = template.Variables.GetIntOrDefault(FantasyAbilityTemplateVariableTypes.ManaCost, 0);
            valid.Add((template, manaCost));
        }

        return valid;
    }

    private bool TryPickAndExecuteAbility(BattleEntity entity)
    {
        var affordable = GetValidAbilities(entity)
            .Where(x => entity.Mana >= x.ManaCost)
            .ToList();

        if (affordable.Count == 0) return false;

        var pick = affordable[_rng.Next(affordable.Count)];
        ExecuteAbility(entity, pick.Template);
        return true;
    }

    private void ExecuteAbility(BattleEntity attacker, AbilityTemplate template, List<BattleEntity> specificTargets = null)
    {
        var baseDamage = template.Variables.GetAsOrDefault<Damage>(CombatAbilityTemplateVariableTypes.Damage, () => new Damage(0, 0));
        var isHealing = baseDamage.Type >= 90;
        var targetType = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetType, 1);
        var targetCount = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetCount, 1);
        var manaCost = template.Variables.GetIntOrDefault(FantasyAbilityTemplateVariableTypes.ManaCost, 0);

        attacker.Entity.State.DeductMana(manaCost, attacker.MaxMana);

        List<BattleEntity> selected;
        if (specificTargets != null && specificTargets.Count > 0)
        {
            selected = specificTargets.Where(t => t.IsAlive).ToList();
            if (selected.Count == 0) return;
        }
        else
        {
            // Healing abilities target allies; damage abilities target enemies
            var pool = isHealing
                ? (attacker.Team == Team.Player ? _party : _enemies)
                : (attacker.Team == Team.Player ? _enemies : _party);
            var aliveTargets = pool.Where(e => e.IsAlive).ToList();
            var actualTargetCount = targetType == CombatTargetTypes.MultipleTarget
                ? Math.Min(targetCount, aliveTargets.Count) : 1;
            if (actualTargetCount == 0) return;
            selected = aliveTargets.OrderBy(_ => _rng.Next()).Take(actualTargetCount).ToList();
        }

        CurrentTargets = selected;

        var abilityName = _localeDataSource.Get("en-gb", template.NameLocaleId);
        var attackerName = NameHelper.NormalizeName(attacker.Name);

        if (isHealing)
        {
            var totalHeal = 0;
            foreach (var target in selected)
            {
                var healValue = (int)Math.Max(1, baseDamage.Value);
                var newHp = Math.Min(target.Hp + healValue, target.MaxHp);
                var actualHeal = newHp - target.Hp;
                target.Hp = newHp;
                OnDamageDealt?.Invoke(target, -actualHeal, false);
                totalHeal += actualHeal;
            }

            var avgHeal = selected.Count > 0 ? totalHeal / selected.Count : 0;
            var targetNames = selected.Select(t => NameHelper.NormalizeName(t.Name)).ToList();
            LastActionMessage = selected.Count == 1
                ? $"{attackerName} uses {abilityName} on {targetNames[0]}, healing {avgHeal} HP!"
                : $"{attackerName} uses {abilityName} on {string.Join(", ", targetNames)}, healing {avgHeal} HP each!";
        }
        else
        {
            var damageCopy = new Damage(baseDamage.Type, baseDamage.Value);
            var attack = _attackGenerator.GenerateAttack(damageCopy, attacker.Entity.Stats);

            var totalDamage = 0;
            foreach (var target in selected)
            {
                var processed = _attackProcessor.ProcessAttack(attack, target.Entity.Stats);
                var dmg = (int)Math.Max(1, processed.DamageDone.Sum(d => d.Value));
                target.Entity.State.DeductHealth(dmg);
                OnDamageDealt?.Invoke(target, dmg, attack.IsCritical);
                totalDamage += dmg;
            }

            var avgDamage = selected.Count > 0 ? totalDamage / selected.Count : 0;
            var targetNames = selected.Select(t => NameHelper.NormalizeName(t.Name)).ToList();
            var critSuffix = attack.IsCritical ? " (CRIT!)" : "";
            LastActionMessage = selected.Count == 1
                ? $"{attackerName} uses {abilityName} on {targetNames[0]} for {avgDamage} damage{critSuffix}"
                : $"{attackerName} uses {abilityName} on {string.Join(", ", targetNames)} for {avgDamage} damage each{critSuffix}";
        }
    }

    private void ExecuteBasicAttack(BattleEntity specificTarget = null)
    {
        var targets = CurrentAttacker.Team == Team.Player ? _enemies : _party;
        var aliveTargets = targets.Where(e => e.IsAlive).ToList();

        BattleEntity target;
        if (specificTarget != null && specificTarget.IsAlive)
            target = specificTarget;
        else
            target = aliveTargets[_rng.Next(aliveTargets.Count)];
        CurrentTargets = [target];

        var attack = _attackGenerator.GenerateAttack(CurrentAttacker.Entity.Stats);
        var processed = _attackProcessor.ProcessAttack(attack, target.Entity.Stats);
        var totalDamage = (int)Math.Max(1, processed.DamageDone.Sum(d => d.Value));

        target.Entity.State.DeductHealth(totalDamage);
        OnDamageDealt?.Invoke(target, totalDamage, attack.IsCritical);
        var critSuffix = attack.IsCritical ? " (CRIT!)" : "";
        LastActionMessage = $"{NameHelper.NormalizeName(CurrentAttacker.Name)} attacks {NameHelper.NormalizeName(target.Name)} for {totalDamage} damage{critSuffix}";
    }

    private void UseItemOnTarget(ItemData itemData, BattleEntity target)
    {
        var template = _dataSource.Get<ItemTemplate>(itemData.TemplateId);
        if (template == null)
        {
            LastActionMessage = "Item not found!";
            return;
        }

        var itemName = _localeDataSource.Get("en-gb", template.NameLocaleId);
        var attackerName = NameHelper.NormalizeName(CurrentAttacker.Name);
        var targetName = NameHelper.NormalizeName(target.Name);
        var healAmount = 0;
        var manaAmount = 0;
        var reviveAmount = 0;

        if (template.Variables.Effects != null)
        {
            foreach (var effect in template.Variables.Effects)
            {
                if (effect is not StaticEffect se) continue;

                if (se.EffectType == GenreEffectTypes.HealthRestoreAmount)
                {
                    healAmount = (int)se.Potency;
                    var newHp = Math.Min(target.Hp + healAmount, target.MaxHp);
                    var actualHeal = newHp - target.Hp;
                    target.Hp = newHp;
                    OnDamageDealt?.Invoke(target, -actualHeal, false);
                }
                else if (se.EffectType == GenreEffectTypes.HealthRestorePercentage)
                {
                    healAmount = (int)(target.MaxHp * se.Potency);
                    var newHp = Math.Min(target.Hp + healAmount, target.MaxHp);
                    var actualHeal = newHp - target.Hp;
                    target.Hp = newHp;
                    OnDamageDealt?.Invoke(target, -actualHeal, false);
                }
                else if (se.EffectType == FantasyEffectTypes.ManaRestoreAmount)
                {
                    manaAmount = (int)se.Potency;
                    var newMp = (int)Math.Min(target.Mana + manaAmount, target.MaxMana);
                    target.Entity.State.Mana = newMp;
                }
                else if (se.EffectType == GenreEffectTypes.LifeRestoreAmount)
                {
                    var reviveHp = (int)se.Potency;
                    target.Entity.State.RestoreLife(reviveHp, target.MaxHp);
                    reviveAmount = target.Hp;
                    OnDamageDealt?.Invoke(target, -target.Hp, false);
                }
                else if (se.EffectType == GenreEffectTypes.LifeRestorePercentage)
                {
                    var reviveHp = (int)(target.MaxHp * se.Potency);
                    target.Entity.State.RestoreLife(reviveHp, target.MaxHp);
                    reviveAmount = target.Hp;
                    OnDamageDealt?.Invoke(target, -target.Hp, false);
                }
            }
        }

        if (healAmount > 0)
            LastActionMessage = $"{attackerName} uses {itemName} on {targetName}, healing {healAmount} HP!";
        else if (manaAmount > 0)
            LastActionMessage = $"{attackerName} uses {itemName} on {targetName}, restoring {manaAmount} MP!";
        else if (reviveAmount > 0)
            LastActionMessage = $"{attackerName} uses {itemName} on {targetName}, reviving with {reviveAmount} HP!";
        else
            LastActionMessage = $"{attackerName} uses {itemName} on {targetName}.";
    }

    private bool CheckGameOver()
    {
        if (_party.All(e => !e.IsAlive))
        {
            WinningTeam = Team.Enemy;
            return true;
        }
        if (_enemies.All(e => !e.IsAlive))
        {
            WinningTeam = Team.Player;
            return true;
        }
        return false;
    }
}

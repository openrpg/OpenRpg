using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Requirements;
using OpenRpg.Data;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Types;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Requirements;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.BattleScene;

public class TurnManager
{
    private static readonly Random _rng = new();
    private readonly IDataSource _dataSource;
    private readonly ICharacterRequirementChecker _requirementChecker;
    private readonly ILocaleDataSource _localeDataSource;
    private List<BattleEntity> _party;
    private List<BattleEntity> _enemies;
    private double _phaseTimer;

    public enum Phase { Idle, TurnDwell, TargetFlash, GameOver }
    public Phase CurrentPhase { get; private set; } = Phase.Idle;
    public BattleEntity CurrentAttacker { get; private set; }
    public BattleEntity CurrentTarget { get; private set; }
    public Team WinningTeam { get; private set; }
    public int CurrentTurnIndex { get; private set; } = -1;
    public string LastActionMessage { get; private set; } = "";
    public List<BattleEntity> TurnOrder { get; private set; } = [];

    public TurnManager(IDataSource dataSource, ICharacterRequirementChecker requirementChecker, ILocaleDataSource localeDataSource)
    {
        _dataSource = dataSource;
        _requirementChecker = requirementChecker;
        _localeDataSource = localeDataSource;
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
                if (AdvanceTurn())
                {
                    CurrentPhase = Phase.TurnDwell;
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

            case Phase.TargetFlash:
                _phaseTimer += dt;
                if (_phaseTimer >= 0.3)
                {
                    if (CheckGameOver())
                        CurrentPhase = Phase.GameOver;
                    else
                    {
                        CurrentPhase = Phase.Idle;
                        _phaseTimer = 0;
                    }
                }
                break;
        }
    }

    private bool AdvanceTurn()
    {
        var count = TurnOrder.Count;
        for (var i = 0; i < count; i++)
        {
            CurrentTurnIndex = (CurrentTurnIndex + 1) % count;
            CurrentAttacker = TurnOrder[CurrentTurnIndex];
            if (!CurrentAttacker.IsAlive) continue;

            var targets = CurrentAttacker.Team == Team.Player ? _enemies : _party;
            if (targets.Any(e => e.IsAlive)) return true;
        }
        return false;
    }

    private void ExecuteAction()
    {
        if (CurrentAttacker.Team == Team.Player)
        {
            if (TryExecuteAbility(CurrentAttacker))
                return;
        }

        ExecuteBasicAttack();
    }

    private bool TryExecuteAbility(BattleEntity entity)
    {
        if (!entity.Entity.Variables.ContainsKey(CombatTemplateVariableTypes.Abilities))
            return false;

        var abilities = entity.Entity.Variables.GetAs<List<AbilityData>>(CombatTemplateVariableTypes.Abilities);
        if (abilities == null || abilities.Count == 0)
            return false;

        var validAbilities = new List<(AbilityData Data, AbilityTemplate Template)>();

        foreach (var abilityData in abilities)
        {
            var template = _dataSource.Get<AbilityTemplate>(abilityData.TemplateId);
            if (template == null) continue;

            var requirements = template.Variables.GetAsOrDefault<IReadOnlyCollection<Requirement>>(CoreTemplateVariableTypes.Requirements, () => Array.Empty<Requirement>());
            if (!_requirementChecker.AreRequirementsMet(entity.Entity, requirements))
                continue;

            var manaCost = template.Variables.GetIntOrDefault(FantasyAbilityTemplateVariableTypes.ManaCost, 0);
            if (entity.Mana < manaCost)
                continue;

            validAbilities.Add((abilityData, template));
        }

        if (validAbilities.Count == 0) return false;

        var pick = validAbilities[_rng.Next(validAbilities.Count)];
        ExecuteAbility(entity, pick.Template);
        return true;
    }

    private void ExecuteAbility(BattleEntity attacker, AbilityTemplate template)
    {
        var damage = template.Variables.GetAsOrDefault<Damage>(CombatAbilityTemplateVariableTypes.Damage, () => new Damage(0, 0));
        var targetType = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetType, 1);
        var targetCount = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetCount, 1);
        var manaCost = template.Variables.GetIntOrDefault(FantasyAbilityTemplateVariableTypes.ManaCost, 0);

        attacker.Entity.State.DeductMana(manaCost, attacker.MaxMana);

        var targets = attacker.Team == Team.Player ? _enemies : _party;
        var aliveTargets = targets.Where(e => e.IsAlive).ToList();

        var actualTargetCount = targetType == CombatTargetTypes.MultipleTarget
            ? Math.Min(targetCount, aliveTargets.Count) : 1;
        if (actualTargetCount == 0) return;

        var selected = aliveTargets.OrderBy(_ => _rng.Next()).Take(actualTargetCount).ToList();
        var abilityName = _localeDataSource.Get("en-gb", template.NameLocaleId);
        var attackerName = NormalizeName(attacker.Name);
        var damageVal = (int)Math.Max(1, damage.Value);

        foreach (var target in selected)
            target.Entity.State.DeductHealth(damageVal);

        var targetNames = selected.Select(t => NormalizeName(t.Name)).ToList();
        if (selected.Count == 1)
            LastActionMessage = $"{attackerName} uses {abilityName} on {targetNames[0]} for {damageVal} damage";
        else
            LastActionMessage = $"{attackerName} uses {abilityName} on {string.Join(", ", targetNames)} for {damageVal} damage each";
    }

    private void ExecuteBasicAttack()
    {
        var targets = CurrentAttacker.Team == Team.Player ? _enemies : _party;
        var aliveTargets = targets.Where(e => e.IsAlive).ToList();
        CurrentTarget = aliveTargets[_rng.Next(aliveTargets.Count)];

        var damage = CurrentAttacker.AttackDamage > 0
            ? CurrentAttacker.AttackDamage
            : _rng.Next(5, 15);
        CurrentTarget.Entity.State.DeductHealth(damage);
        LastActionMessage = $"{NormalizeName(CurrentAttacker.Name)} attacks {NormalizeName(CurrentTarget.Name)} for {damage} damage";
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

    private static string NormalizeName(string name)
    {
        return Regex.Replace(name, "(?<=[a-z])(?=[A-Z])", " ");
    }
}

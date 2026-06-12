using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Processors.Attacks.Entity;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Types;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Requirements;
using OpenRpg.Items.Templates;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Combat;

public class TurnManager
{
    private static readonly Random _rng = new();
    private readonly IDataSource _dataSource;
    private readonly ILocaleDataSource _localeDataSource;
    private readonly AbilityExecutor _abilityExecutor;
    private readonly BasicAttackExecutor _basicAttackExecutor;
    private readonly ItemEffectApplier _itemEffectApplier;
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
        _localeDataSource = localeDataSource;
        _abilityExecutor = new AbilityExecutor(dataSource, requirementChecker, localeDataSource, attackGenerator, attackProcessor);
        _basicAttackExecutor = new BasicAttackExecutor(attackGenerator, attackProcessor);
        _itemEffectApplier = new ItemEffectApplier();
    }

    public void Start(List<BattleEntity> party, List<BattleEntity> enemies)
    {
        _party = party;
        _enemies = enemies;
        _abilityExecutor.Start(party, enemies);
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
                if (_phaseTimer >= BattlerConstants.TurnDwellSeconds)
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
                if (_phaseTimer >= BattlerConstants.TargetFlashSeconds)
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
                        var result = _abilityExecutor.ExecuteAbility(CurrentAttacker, action.Ability, action.Targets);
                        ApplyAbilityResult(result);
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
            if (_abilityExecutor.TryPickAndExecuteAbility(CurrentAttacker, out var result))
            {
                ApplyAbilityResult(result);
                return;
            }
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
        return _abilityExecutor.GetAvailableAbilities(entity);
    }

    private void ApplyAbilityResult(AbilityExecutor.AbilityResult result)
    {
        CurrentTargets = result.Targets;
        LastActionMessage = result.Message;
        foreach (var (target, amount, isCrit) in result.DamageEvents)
            OnDamageDealt?.Invoke(target, amount, isCrit);
    }

    private void ExecuteBasicAttack(BattleEntity specificTarget = null)
    {
        var targets = CurrentAttacker.Team == Team.Player ? _enemies : _party;
        var result = _basicAttackExecutor.ExecuteBasicAttack(CurrentAttacker, specificTarget, targets);

        CurrentTargets = [result.Target];
        OnDamageDealt?.Invoke(result.Target, result.Damage, result.IsCrit);
        LastActionMessage = result.Message;
    }

    private void UseItemOnTarget(ItemData itemData, BattleEntity target)
    {
        var template = _dataSource.Get<ItemTemplate>(itemData.TemplateId);
        if (template == null)
        {
            LastActionMessage = "Item not found!";
            return;
        }

        var result = _itemEffectApplier.ApplyItemEffects(itemData, target, template);
        var itemName = _localeDataSource.Get("en-gb", template.NameLocaleId);
        var attackerName = UI.NameHelper.NormalizeName(CurrentAttacker.Name);
        var targetName = UI.NameHelper.NormalizeName(target.Name);

        LastActionMessage = CombatLogBuilder.BuildItemMessage(attackerName, itemName, targetName, result.HealAmount, result.ManaAmount, result.ReviveAmount);

        if (result.HealAmount > 0 || result.ReviveAmount > 0 || result.ManaAmount > 0)
            OnDamageDealt?.Invoke(target, -(result.HealAmount + result.ReviveAmount), false);
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

using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Combat.Processors.Attacks.Entity;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Requirements;
using OpenRpg.Data;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Genres.Types;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;
using OpenRpg.Entities.Types;
using OpenRpg.Genres.Requirements;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Combat;

public class AbilityExecutor
{
    private static readonly Random _rng = new();
    private readonly IDataSource _dataSource;
    private readonly ICharacterRequirementChecker _requirementChecker;
    private readonly ILocaleDataSource _localeDataSource;
    private readonly IEntityAttackGenerator _attackGenerator;
    private readonly IEntityAttackProcessor _attackProcessor;

    private List<BattleEntity> _party;
    private List<BattleEntity> _enemies;

    public record AbilityResult(
        IReadOnlyList<BattleEntity> Targets,
        string Message,
        IReadOnlyList<(BattleEntity Target, int Amount, bool IsCrit)> DamageEvents);

    public AbilityExecutor(
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

    public List<(AbilityTemplate Template, int ManaCost)> GetValidAbilities(BattleEntity entity)
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

    public bool TryPickAndExecuteAbility(BattleEntity entity, out AbilityResult result)
    {
        result = null;
        var affordable = GetValidAbilities(entity)
            .Where(x => entity.Mana >= x.ManaCost)
            .ToList();

        if (affordable.Count == 0) return false;

        var pick = affordable[_rng.Next(affordable.Count)];
        result = ExecuteAbility(entity, pick.Template);
        return true;
    }

    public AbilityResult ExecuteAbility(BattleEntity attacker, AbilityTemplate template, List<BattleEntity> specificTargets = null)
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
            if (selected.Count == 0)
                return new AbilityResult([], "", []);
        }
        else
        {
            var pool = isHealing
                ? (attacker.Team == Team.Player ? _party : _enemies)
                : (attacker.Team == Team.Player ? _enemies : _party);
            var aliveTargets = pool.Where(e => e.IsAlive).ToList();
            var actualTargetCount = targetType == CombatTargetTypes.MultipleTarget
                ? Math.Min(targetCount, aliveTargets.Count) : 1;
            if (actualTargetCount == 0)
                return new AbilityResult([], "", []);
            selected = aliveTargets.OrderBy(_ => _rng.Next()).Take(actualTargetCount).ToList();
        }

        var attackerName = NameHelper.NormalizeName(attacker.Name);
        var abilityName = _localeDataSource.Get("en-gb", template.NameLocaleId);
        var damageEvents = new List<(BattleEntity, int, bool)>();
        var totalValue = 0;

        if (isHealing)
        {
            foreach (var target in selected)
            {
                var healValue = (int)Math.Max(1, baseDamage.Value);
                var newHp = Math.Min(target.Hp + healValue, target.MaxHp);
                var actualHeal = newHp - target.Hp;
                target.Hp = newHp;
                damageEvents.Add((target, -actualHeal, false));
                totalValue += actualHeal;
            }

            var healMsg = CombatLogBuilder.BuildAbilityMessage(attackerName, abilityName, selected, totalValue, false, true);
            return new AbilityResult(selected, healMsg, damageEvents);
        }
        else
        {
            var damageCopy = new Damage(baseDamage.Type, baseDamage.Value);
            var attack = _attackGenerator.GenerateAttack(damageCopy, attacker.Entity.Stats);

            foreach (var target in selected)
            {
                var processed = _attackProcessor.ProcessAttack(attack, target.Entity.Stats);
                var dmg = (int)Math.Max(1, processed.DamageDone.Sum(d => d.Value));
                target.Entity.State.DeductHealth(dmg);
                damageEvents.Add((target, dmg, attack.IsCritical));
                totalValue += dmg;
            }

            var msg = CombatLogBuilder.BuildAbilityMessage(attackerName, abilityName, selected, totalValue, attack.IsCritical, false);
            return new AbilityResult(selected, msg, damageEvents);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Extensions;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Extensions;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Genres.Fantasy.Types;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Combat;

/// <summary>
/// Resolves targets for abilities based on template data (TargetType, TargetCount, Damage type).
/// Shared between AbilityExecutor and CommandMenuUi to eliminate duplication.
/// </summary>
public static class TargetResolver
{
    public static bool IsHealing(AbilityTemplate template)
    {
        var damage = template.Variables.GetAsOrDefault<Damage>(
            CombatAbilityTemplateVariableTypes.Damage, () => new Damage(0, 0));
        return damage.Type == FantasyDamageTypes.LightDamage;
    }

    public static bool IsHealing(Damage damage)
    {
        return damage.Type == FantasyDamageTypes.LightDamage;
    }

    public static List<BattleEntity> ResolveTargets(
        AbilityTemplate template,
        BattleEntity attacker,
        List<BattleEntity> specificTargets,
        List<BattleEntity> party,
        List<BattleEntity> enemies)
    {
        var isHealing = IsHealing(template);
        var targetType = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetType, 1);
        var targetCount = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetCount, 1);

        if (specificTargets != null && specificTargets.Count > 0)
        {
            return specificTargets.Where(t => t.IsAlive).ToList();
        }

        var pool = isHealing
            ? (attacker.Team == Team.Player ? party : enemies)
            : (attacker.Team == Team.Player ? enemies : party);

        var aliveTargets = pool.Where(e => e.IsAlive).ToList();
        var actualTargetCount = targetType == CombatTargetTypes.MultipleTarget
            ? Math.Min(targetCount, aliveTargets.Count) : 1;

        if (actualTargetCount == 0) return [];

        var rng = new Random();
        return aliveTargets.OrderBy(_ => rng.Next()).Take(actualTargetCount).ToList();
    }

    public static List<BattleEntity> ResolvePreviewTargets(
        AbilityTemplate template,
        List<BattleEntity> aliveParty,
        List<BattleEntity> aliveEnemies)
    {
        var isHealing = IsHealing(template);
        var targetType = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetType, 1);

        if (targetType != CombatTargetTypes.MultipleTarget)
        { return []; }

        var pool = isHealing ? aliveParty : aliveEnemies;
        if (pool == null) { return []; }

        var targetCount = template.Variables.GetIntOrDefault(CombatAbilityTemplateVariableTypes.TargetCount, 1);
        var actualCount = Math.Min(targetCount, pool.Count);
        return pool.Take(actualCount).ToList();
    }

    public static List<BattleEntity> GetTargetPool(
        bool isHealing,
        BattleEntity attacker,
        List<BattleEntity> party,
        List<BattleEntity> enemies)
    {
        return isHealing
            ? (attacker.Team == Team.Player ? party : enemies)
            : (attacker.Team == Team.Player ? enemies : party);
    }
}

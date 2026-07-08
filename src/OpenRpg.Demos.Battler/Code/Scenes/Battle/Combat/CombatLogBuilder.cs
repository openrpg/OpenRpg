using System.Collections.Generic;
using System.Linq;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.UI;

namespace OpenRpg.Demos.Battler.Code.Scenes.Battle.Combat;

public static class CombatLogBuilder
{
    public static string BuildAbilityMessage(string attackerName, string abilityName, List<BattleEntity> targets, int totalValue, bool isCrit, bool isHealing)
    {
        var targetNames = targets.Select(t => NameHelper.NormalizeName(t.Name)).ToList();
        var avg = targets.Count > 0 ? totalValue / targets.Count : 0;
        var targetList = targets.Count == 1
            ? targetNames[0]
            : string.Join(", ", targetNames);

        if (isHealing)
        {
            return targets.Count == 1
                ? $"{attackerName} uses {abilityName} on {targetList}, healing {avg} HP!"
                : $"{attackerName} uses {abilityName} on {targetList}, healing {avg} HP each!";
        }

        var critSuffix = isCrit ? " (CRIT!)" : "";
        return targets.Count == 1
            ? $"{attackerName} uses {abilityName} on {targetList} for {avg} damage{critSuffix}"
            : $"{attackerName} uses {abilityName} on {targetList} for {avg} damage each{critSuffix}";
    }

    public static string BuildAttackMessage(string attackerName, string targetName, int damage, bool isCrit)
    {
        var critSuffix = isCrit ? " (CRIT!)" : "";
        return $"{attackerName} attacks {targetName} for {damage} damage{critSuffix}";
    }

    public static string BuildItemMessage(string attackerName, string itemName, string targetName, int healAmount, int manaAmount, int reviveAmount)
    {
        if (healAmount > 0)
            return $"{attackerName} uses {itemName} on {targetName}, healing {healAmount} HP!";
        if (manaAmount > 0)
            return $"{attackerName} uses {itemName} on {targetName}, restoring {manaAmount} MP!";
        if (reviveAmount > 0)
            return $"{attackerName} uses {itemName} on {targetName}, reviving with {reviveAmount} HP!";
        return $"{attackerName} uses {itemName} on {targetName}.";
    }
}

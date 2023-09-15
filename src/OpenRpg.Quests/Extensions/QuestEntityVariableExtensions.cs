using OpenRpg.Core.Variables.Entity;
using OpenRpg.Quests.Factions;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Extensions
{
    public static class QuestEntityVariableExtensions
    {
        public static bool HasFactionReputation(this IEntityVariables vars) 
        { return vars.ContainsKey(QuestEntityVariableTypes.FactionReputation); }
        
        public static IFactionReputation FactionReputation(this IEntityVariables vars)
        { return vars[QuestEntityVariableTypes.FactionReputation] as IFactionReputation; }

        public static void FactionReputation(this IEntityVariables vars, IFactionReputation factionReputation)
        { vars[QuestEntityVariableTypes.FactionReputation] = factionReputation; }
    }
}
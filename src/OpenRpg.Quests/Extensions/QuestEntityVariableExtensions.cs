using OpenRpg.Core.Extensions;
using OpenRpg.Core.Variables.Entity;
using OpenRpg.Quests.Factions;
using OpenRpg.Quests.State;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Extensions
{
    public static class QuestEntityVariableExtensions
    {
        public static bool HasFactionReputation(this IEntityVariables vars) 
        { return vars.ContainsKey(QuestEntityVariableTypes.FactionReputation); }
        
        public static IFactionReputation FactionReputation(this IEntityVariables vars)
        { return vars.GetAs<IFactionReputation>(QuestEntityVariableTypes.FactionReputation); }

        public static void FactionReputation(this IEntityVariables vars, IFactionReputation factionReputation)
        { vars[QuestEntityVariableTypes.FactionReputation] = factionReputation; }
        
        public static bool HasQuestState(this IEntityVariables vars) 
        { return vars.ContainsKey(QuestEntityVariableTypes.QuestState); }
        
        public static IQuestState QuestState(this IEntityVariables vars)
        { return vars.GetAs<IQuestState>(QuestEntityVariableTypes.QuestState); }

        public static void QuestState(this IEntityVariables vars, IQuestState questState)
        { vars[QuestEntityVariableTypes.QuestState] = questState; }
        
        public static bool HasTriggerState(this IEntityVariables vars) 
        { return vars.ContainsKey(QuestEntityVariableTypes.TriggerState); }
        
        public static ITriggerStateVariables TriggerState(this IEntityVariables vars)
        { return vars.GetAs<ITriggerStateVariables>(QuestEntityVariableTypes.TriggerState); }

        public static void TriggerState(this IEntityVariables vars, ITriggerStateVariables triggerState)
        { vars[QuestEntityVariableTypes.TriggerState] = triggerState; }
    }
}
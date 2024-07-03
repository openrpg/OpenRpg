using OpenRpg.Core.Entity.Variables;
using OpenRpg.Core.Extensions;
using OpenRpg.Quests.Factions;
using OpenRpg.Quests.State;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Extensions
{
    public static class QuestEntityVariableExtensions
    {
        public static bool HasFactionReputation(this EntityVariables vars) 
        { return vars.ContainsKey(QuestEntityVariableTypes.FactionReputation); }
        
        public static FactionReputation FactionReputation(this EntityVariables vars)
        { return vars.GetAs<FactionReputation>(QuestEntityVariableTypes.FactionReputation); }

        public static void FactionReputation(this EntityVariables vars, FactionReputation factionReputation)
        { vars[QuestEntityVariableTypes.FactionReputation] = factionReputation; }
        
        public static bool HasQuestState(this EntityVariables vars) 
        { return vars.ContainsKey(QuestEntityVariableTypes.QuestState); }
        
        public static IQuestState QuestState(this EntityVariables vars)
        { return vars.GetAs<IQuestState>(QuestEntityVariableTypes.QuestState); }

        public static void QuestState(this EntityVariables vars, IQuestState questState)
        { vars[QuestEntityVariableTypes.QuestState] = questState; }
        
        public static bool HasTriggerState(this EntityVariables vars) 
        { return vars.ContainsKey(QuestEntityVariableTypes.TriggerState); }
        
        public static ITriggerStateVariables TriggerState(this EntityVariables vars)
        { return vars.GetAs<ITriggerStateVariables>(QuestEntityVariableTypes.TriggerState); }

        public static void TriggerState(this EntityVariables vars, ITriggerStateVariables triggerState)
        { vars[QuestEntityVariableTypes.TriggerState] = triggerState; }
    }
}
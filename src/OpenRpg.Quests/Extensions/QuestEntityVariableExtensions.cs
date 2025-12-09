using OpenRpg.Core.Extensions;
using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Quests.Factions;
using OpenRpg.Quests.State;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Extensions
{
    public static class QuestEntityVariableExtensions
    {
        public static bool HasFactionReputation(this EntityVariables vars) 
        { return vars.ContainsKey(QuestEntityVariableTypes.FactionReputation); }

        extension(EntityVariables vars)
        {
            public FactionReputation FactionReputation
            {
                get => vars.GetAsOrDefault(QuestEntityVariableTypes.FactionReputation, () => new FactionReputation());
                set =>  vars[QuestEntityVariableTypes.FactionReputation] = value;
            }
        }
        
        public static bool HasQuestState(this EntityVariables vars) 
        { return vars.ContainsKey(QuestEntityVariableTypes.QuestState); }
        
        extension(EntityVariables vars)
        {
            public IQuestState QuestState
            {
                get => vars.GetAsOrDefault(QuestEntityVariableTypes.QuestState, () => new QuestState());
                set =>  vars[QuestEntityVariableTypes.QuestState] = value;
            }
        }
        
        public static bool HasTriggerState(this EntityVariables vars) 
        { return vars.ContainsKey(QuestEntityVariableTypes.TriggerState); }
        
        extension(EntityVariables vars)
        {
            public ITriggerState TriggerState
            {
                get => vars.GetAsOrDefault(QuestEntityVariableTypes.TriggerState, () => new TriggerState());
                set =>  vars[QuestEntityVariableTypes.TriggerState] = value;
            }
        }
    }
}
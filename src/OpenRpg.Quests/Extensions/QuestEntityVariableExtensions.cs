using System.Collections.Generic;
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
                get => vars.GetAsOrDefaultAndSet(QuestEntityVariableTypes.FactionReputation, () => new FactionReputation());
                set => vars[QuestEntityVariableTypes.FactionReputation] = value;
            }
        }

        public static bool HasTriggerState(this EntityVariables vars)
        { return vars.ContainsKey(QuestEntityVariableTypes.TriggerState); }

        extension(EntityVariables vars)
        {
            public ITriggerState TriggerState
            {
                get => vars.GetAsOrDefaultAndSet(QuestEntityVariableTypes.TriggerState, () => new TriggerState());
                set => vars[QuestEntityVariableTypes.TriggerState] = value;
            }
        }

        public static bool HasActiveQuests(this EntityVariables vars)
        { return vars.ContainsKey(QuestEntityVariableTypes.ActiveQuests); }

        extension(EntityVariables vars)
        {
            public List<QuestData> ActiveQuests
            {
                get => vars.GetAsOrDefaultAndSet(QuestEntityVariableTypes.ActiveQuests, () => new List<QuestData>());
                set => vars[QuestEntityVariableTypes.ActiveQuests] = value;
            }
        }

        public static QuestData FindQuestData(this EntityVariables vars, int templateId)
        {
            if (!vars.ContainsKey(QuestEntityVariableTypes.ActiveQuests)) return null;
            var quests = vars.ActiveQuests;
            for (var i = 0; i < quests.Count; i++)
            {
                if (quests[i].TemplateId == templateId)
                    return quests[i];
            }
            return null;
        }
    }
}

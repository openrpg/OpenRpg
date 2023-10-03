using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.State
{
    public class DefaultQuestState : DefaultVariables<int>, IQuestState
    {
        public int GetQuestState(int questId)
        { return this.GetValueOrDefault(questId, QuestStateTypes.QuestNotStarted); }

        public DefaultQuestState(IDictionary<int, int> internalVariables = null) : base(QuestVariableTypes.QuestStateVariables, internalVariables)
        {
        }
    }
}
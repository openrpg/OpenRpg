using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.State
{
    public class QuestState : Variables<int>, IQuestState
    {
        public QuestState(IDictionary<int, int> internalVariables = null) : base(QuestVariableTypes.QuestStateVariables, internalVariables)
        {
        }
    }
}
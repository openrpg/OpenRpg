using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Variables
{
    public class DefaultQuestStateVariables : DefaultVariables<int>, IQuestStateVariables
    {
        public DefaultQuestStateVariables(IDictionary<int, int> internalVariables = null) : base(QuestVariableTypes.QuestStateVariables, internalVariables)
        {
        }
    }
}
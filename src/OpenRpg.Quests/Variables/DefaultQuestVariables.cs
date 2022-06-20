using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Variables
{
    public class DefaultQuestVariables : DefaultVariables<object>, IQuestVariables
    {
        public DefaultQuestVariables(IDictionary<int, object> internalVariables = null) : base(QuestVariableTypes.QuestVariables, internalVariables)
        {
        }
    }
}
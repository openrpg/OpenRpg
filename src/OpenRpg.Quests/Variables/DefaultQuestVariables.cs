using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Variables
{
    public class DefaultQuestVariables : ObjectVariables, IQuestVariables
    {
        public DefaultQuestVariables(IDictionary<int, object> internalVariables = null) : base(QuestVariableTypes.QuestVariables, internalVariables)
        {
        }
    }
}
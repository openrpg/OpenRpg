using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Variables
{
    public class DefaultTriggerStateVariables : DefaultVariables<bool>, ITriggerStateVariables
    {
        public DefaultTriggerStateVariables(IDictionary<int, bool> internalVariables = null) : base(QuestVariableTypes.TriggerVariables, internalVariables)
        {
        }
    }
}
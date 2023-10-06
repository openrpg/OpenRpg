using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.State
{
    public class DefaultTriggerState : DefaultVariables<bool>, ITriggerStateVariables
    {
        public DefaultTriggerState(IDictionary<int, bool> internalVariables = null) : base(QuestVariableTypes.TriggerVariables, internalVariables)
        {
        }
    }
}
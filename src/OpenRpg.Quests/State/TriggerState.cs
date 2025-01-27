using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.State
{
    public class TriggerState : Variables<bool>, ITriggerStateVariables
    {
        public TriggerState(IDictionary<int, bool> internalVariables = null) : base(QuestVariableTypes.TriggerVariables, internalVariables)
        {
        }
    }
}
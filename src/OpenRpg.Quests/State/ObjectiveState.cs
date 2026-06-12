using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.State
{
    public class ObjectiveState : Variables<int>, IObjectiveState
    {
        public ObjectiveState(IDictionary<int, int> internalVariables = null) : base(QuestVariableTypes.ObjectiveStateVariables, internalVariables)
        {
        }
    }
}

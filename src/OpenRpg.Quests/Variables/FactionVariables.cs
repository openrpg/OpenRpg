using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Quests.Types;

namespace OpenRpg.Quests.Variables
{
    public class FactionVariables : ObjectVariables
    {
        public FactionVariables(IDictionary<int, object> internalVariables = null) : base(QuestVariableTypes.FactionVariables, internalVariables)
        {
        }
    }
}
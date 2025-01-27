using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Races.Variables
{
    public class RaceVariables : ObjectVariables
    {
        public RaceVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.RaceVariables, internalVariables)
        {
        }
    }
}
using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Races.Variables
{
    public class RaceVariables : ObjectVariables
    {
        public RaceVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.RaceVariables, internalVariables)
        {
        }
    }
}
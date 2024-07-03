using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Genres.Scifi.Types;

namespace OpenRpg.Genres.Scifi.Variables
{
    public class ShipVariables : ObjectVariables
    {
        public ShipVariables(IDictionary<int, object> internalVariables = null) : base(ScifiVariableTypes.ShipVariables, internalVariables)
        {
        }
    }
}
using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Genres.Scifi.Types;

namespace OpenRpg.Genres.Scifi.Variables
{
    public class DefaultShipVariables : ObjectVariables, IShipVariables
    {
        public DefaultShipVariables(IDictionary<int, object> internalVariables = null) : base(ScifiVariableTypes.ShipVariables, internalVariables)
        {
        }
    }
}
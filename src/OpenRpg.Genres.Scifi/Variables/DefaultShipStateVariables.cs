using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Genres.Scifi.Types;

namespace OpenRpg.Genres.Scifi.Variables
{
    public class DefaultShipStateVariables : DefaultVariables<float>, IShipStateVariables
    {
        public DefaultShipStateVariables(IDictionary<int, float> internalVariables = null) : base(ScifiVariableTypes.ShipStatsVariables, internalVariables)
        {
        }
    }
}
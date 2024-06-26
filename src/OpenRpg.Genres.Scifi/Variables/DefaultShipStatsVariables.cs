using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Genres.Scifi.Types;

namespace OpenRpg.Genres.Scifi.Variables
{
    public class DefaultShipStatsVariables : DefaultVariables<float>, IShipStatsVariables
    {
        public DefaultShipStatsVariables(IDictionary<int, float> internalVariables = null) : base(ScifiVariableTypes.ShipStatsVariables, internalVariables)
        {
        }
    }
}
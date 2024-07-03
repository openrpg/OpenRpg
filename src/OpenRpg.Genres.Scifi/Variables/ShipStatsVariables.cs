using System.Collections.Generic;
using OpenRpg.Core.Stats;
using OpenRpg.Core.Variables;
using OpenRpg.Genres.Scifi.Types;

namespace OpenRpg.Genres.Scifi.Variables
{
    public class ShipStatsVariables : Variables<float>, IStatsVariables
    {
        public ShipStatsVariables(IDictionary<int, float> internalVariables = null) : base(ScifiVariableTypes.ShipStatsVariables, internalVariables)
        {
        }
    }
}
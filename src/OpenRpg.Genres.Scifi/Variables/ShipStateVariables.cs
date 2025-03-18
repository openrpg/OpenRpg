using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.State;
using OpenRpg.Genres.Scifi.Types;

namespace OpenRpg.Genres.Scifi.Variables
{
    public class ShipStateVariables : Variables<float>, IStateVariables
    {
        public ShipStateVariables(IDictionary<int, float> internalVariables = null) : base(ScifiVariableTypes.ShipStatsVariables, internalVariables)
        {
        }
    }
}
using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables;

namespace OpenRpg.Core.Stats.Variables
{
    public class DefaultComputedStatsVariables : DefaultVariables<float>, IComputedStatsVariables
    {
        public DefaultComputedStatsVariables(IDictionary<int, float> internalVariables = null) : base(CoreVariableTypes.ComputedStatsVariables, internalVariables)
        {
        }
    }
}
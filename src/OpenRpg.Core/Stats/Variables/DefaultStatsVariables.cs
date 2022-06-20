using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables;

namespace OpenRpg.Core.Stats.Variables
{
    public class DefaultStatsVariables : DefaultVariables<float>, IStatsVariables
    {
        public DefaultStatsVariables(IDictionary<int, float> internalVariables = null) : base(CoreVariableTypes.StatsVariables, internalVariables)
        {
        }
    }
}
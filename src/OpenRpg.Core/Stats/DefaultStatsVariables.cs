using System.Collections.Generic;
using OpenRpg.Core.Variables;

namespace OpenRpg.Core.Stats
{
    public class DefaultStatsVariables : DefaultVariables<float>, IStatsVariables
    {
        public DefaultStatsVariables(IDictionary<int, float> internalVariables = null) : base(internalVariables)
        {
        }
    }
}
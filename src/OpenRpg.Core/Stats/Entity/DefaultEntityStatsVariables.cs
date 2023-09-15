using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables;

namespace OpenRpg.Core.Stats.Entity
{
    public class DefaultEntityStatsVariables : DefaultVariables<float>, IEntityStatsVariables
    {
        public DefaultEntityStatsVariables(IDictionary<int, float> internalVariables = null) : base(CoreVariableTypes.EntityStateVariables, internalVariables)
        {
        }
    }
}
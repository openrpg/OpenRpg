using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables;

namespace OpenRpg.Core.Stats.Variables
{
    public class EntityStatsVariables : Variables<float>, IStatsVariables
    {
        public EntityStatsVariables(IDictionary<int, float> internalVariables = null) : base(CoreVariableTypes.EntityStatsVariables, internalVariables)
        {
        }
    }
}
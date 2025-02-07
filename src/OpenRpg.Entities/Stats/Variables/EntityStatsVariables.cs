using System.Collections.Generic;
using OpenRpg.Entities.Types;
using FloatVariables = OpenRpg.Core.Variables.Variables<float>;

namespace OpenRpg.Entities.Stats.Variables
{
    public class EntityStatsVariables : FloatVariables, IStatsVariables
    {
        public EntityStatsVariables(IDictionary<int, float> internalVariables = null) : base(CoreVariableTypes.EntityStatsVariables, internalVariables)
        {
        }
    }
}
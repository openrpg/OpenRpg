using System.Collections.Generic;
using OpenRpg.Entities.Types;
using FloatVariables = OpenRpg.Core.Variables.Variables<float>;

namespace OpenRpg.Entities.State.Variables
{
    public class EntityStateVariables : FloatVariables, IStateVariables
    {
        public EntityStateVariables(IDictionary<int, float> internalVariables = null) : base(CoreVariableTypes.EntityStateVariables, internalVariables)
        {
        }
    }
}
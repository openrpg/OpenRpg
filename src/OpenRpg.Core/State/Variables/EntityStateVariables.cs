using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables;

namespace OpenRpg.Core.State.Variables
{
    public class EntityStateVariables : Variables<float>, IStateVariables
    {
        public EntityStateVariables(IDictionary<int, float> internalVariables = null) : base(CoreVariableTypes.EntityStateVariables, internalVariables)
        {
        }
    }
}
using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables;

namespace OpenRpg.Core.State.Entity
{
    public class DefaultEntityStateVariables : DefaultVariables<float>, IEntityStateVariables
    {
        public DefaultEntityStateVariables(IDictionary<int, float> internalVariables = null) : base(CoreVariableTypes.EntityStateVariables, internalVariables)
        {
        }
    }
}
using System.Collections.Generic;
using OpenRpg.Core.Types;

namespace OpenRpg.Core.Variables.Entity
{
    public class DefaultEntityVariables : DefaultVariables<object>, IEntityVariables
    {
        public DefaultEntityVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.EntityVariables, internalVariables)
        {
        }
    }
}
using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Variables.Entity
{
    public class DefaultEntityVariables : ObjectVariables, IEntityVariables
    {
        public DefaultEntityVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.EntityVariables, internalVariables)
        {
        }
    }
}
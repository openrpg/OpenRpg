using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Entity.Variables
{
    public class EntityVariables : ObjectVariables
    {
        public EntityVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.EntityVariables, internalVariables)
        {
        }
    }
}
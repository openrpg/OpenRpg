using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Entity.Variables
{
    public class EntityVariables : ObjectVariables
    {
        public EntityVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.EntityVariables, internalVariables)
        {
        }
    }
}
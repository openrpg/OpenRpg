using System.Collections.Generic;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Entity.Variables
{
    public class EntityVariables : ObjectVariables, ITemplateDataVariables
    {
        public EntityVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.EntityVariables, internalVariables)
        {
        }
    }
}
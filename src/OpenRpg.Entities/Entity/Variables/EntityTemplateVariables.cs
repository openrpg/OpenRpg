using System.Collections.Generic;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Entity.Variables;

public class EntityTemplateVariables : ObjectVariables, ITemplateVariables
{
    public EntityTemplateVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.EntityTemplateVariables, internalVariables)
    {
    }
}
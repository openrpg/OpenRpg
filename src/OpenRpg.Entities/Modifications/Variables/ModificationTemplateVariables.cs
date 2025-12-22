using System.Collections.Generic;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Modifications.Variables
{
    public class ModificationTemplateVariables : ObjectVariables, ITemplateVariables
    {
        public ModificationTemplateVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.ModificationTemplateVariables, internalVariables)
        {
        }
    }
}
using System.Collections.Generic;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Modifications.Variables
{
    public class ModificationVariables : ObjectVariables, ITemplateDataVariables
    {
        public ModificationVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.ModificationVariables, internalVariables)
        {
        }
    }
}
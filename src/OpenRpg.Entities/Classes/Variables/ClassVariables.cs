using System.Collections.Generic;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Classes.Variables
{
    public class ClassVariables : ObjectVariables, ITemplateDataVariables
    {
        public ClassVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.ClassVariables, internalVariables)
        {
        }
    }
}
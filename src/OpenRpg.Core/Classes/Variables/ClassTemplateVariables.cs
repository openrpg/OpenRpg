using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Classes.Variables
{
    public class ClassTemplateVariables : ObjectVariables
    {
        public ClassTemplateVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.ClassTemplateVariables, internalVariables)
        {
        }
    }
}
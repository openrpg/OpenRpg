using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Classes.Variables
{
    public class ClassTemplateVariables : ObjectVariables
    {
        public ClassTemplateVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.ClassTemplateVariables, internalVariables)
        {
        }
    }
}
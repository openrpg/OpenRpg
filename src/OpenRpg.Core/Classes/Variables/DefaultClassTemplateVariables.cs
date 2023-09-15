using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Classes.Variables
{
    public class DefaultClassTemplateVariables : ObjectVariables, IClassTemplateVariables
    {
        public DefaultClassTemplateVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.ClassTemplateVariables, internalVariables)
        {
        }
    }
}
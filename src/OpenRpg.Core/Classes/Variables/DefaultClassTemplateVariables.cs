using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables;

namespace OpenRpg.Core.Classes.Variables
{
    public class DefaultClassTemplateVariables : DefaultVariables<object>, IClassTemplateVariables
    {
        public DefaultClassTemplateVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.ClassTemplateVariables, internalVariables)
        {
        }
    }
}
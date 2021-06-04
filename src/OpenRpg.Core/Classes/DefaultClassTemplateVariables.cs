using System.Collections.Generic;
using OpenRpg.Core.Variables;

namespace OpenRpg.Core.Classes
{
    public class DefaultClassTemplateVariables : DefaultVariables<object>, IClassTemplateVariables
    {
        public DefaultClassTemplateVariables(IDictionary<int, object> internalVariables = null) : base(internalVariables)
        {
        }
    }
}
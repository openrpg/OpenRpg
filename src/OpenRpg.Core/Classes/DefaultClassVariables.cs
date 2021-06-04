using System.Collections.Generic;
using OpenRpg.Core.Variables;

namespace OpenRpg.Core.Classes
{
    public class DefaultClassVariables : DefaultVariables<object>, IClassVariables
    {
        public DefaultClassVariables(IDictionary<int, object> internalVariables = null) : base(internalVariables)
        {
        }
    }
}
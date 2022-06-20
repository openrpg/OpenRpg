using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables;

namespace OpenRpg.Core.Classes.Variables
{
    public class DefaultClassVariables : DefaultVariables<object>, IClassVariables
    {
        public DefaultClassVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.ClassVariables, internalVariables)
        {
        }
    }
}
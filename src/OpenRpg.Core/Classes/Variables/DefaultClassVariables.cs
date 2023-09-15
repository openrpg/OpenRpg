using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Classes.Variables
{
    public class DefaultClassVariables : ObjectVariables, IClassVariables
    {
        public DefaultClassVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.ClassVariables, internalVariables)
        {
        }
    }
}
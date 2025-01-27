using System.Collections.Generic;
using OpenRpg.Core.Types;
using OpenRpg.Core.Variables.General;

namespace OpenRpg.Core.Classes.Variables
{
    public class ClassVariables : ObjectVariables
    {
        public ClassVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.ClassVariables, internalVariables)
        {
        }
    }
}
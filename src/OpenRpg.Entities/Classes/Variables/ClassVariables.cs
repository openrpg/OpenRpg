using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Entities.Types;

namespace OpenRpg.Entities.Classes.Variables
{
    public class ClassVariables : ObjectVariables
    {
        public ClassVariables(IDictionary<int, object> internalVariables = null) : base(CoreVariableTypes.ClassVariables, internalVariables)
        {
        }
    }
}
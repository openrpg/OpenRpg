using System.Collections.Generic;

namespace OpenRpg.Core.Variables.General
{
    public class ObjectVariables : Variables<object>
    {
        public ObjectVariables(int variableType = 0, IDictionary<int, object> internalVariables = null) : base(variableType, internalVariables)
        {
        }
    }
}
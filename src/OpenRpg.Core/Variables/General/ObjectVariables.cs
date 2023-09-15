using System.Collections.Generic;

namespace OpenRpg.Core.Variables.General
{
    public class ObjectVariables : DefaultVariables<object>
    {
        public ObjectVariables(int variableType, IDictionary<int, object> internalVariables = null) : base(variableType, internalVariables)
        {
        }
    }
}
using System.Collections.Generic;

namespace OpenRpg.Core.Variables
{
    public class DefaultVariables<T> : DefaultKeyedVariables<int, T>
    {
        public DefaultVariables(int variableType, IDictionary<int, T> internalVariables = null) : base(variableType, internalVariables)
        { }
    }
}
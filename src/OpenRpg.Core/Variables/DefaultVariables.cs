using System.Collections.Generic;

namespace OpenRpg.Core.Variables
{
    public class DefaultVariables<T> : DefaultKeyedVariables<int, T>
    {
        public DefaultVariables(IDictionary<int, T> internalVariables = null) : base(internalVariables)
        { }
    }
}
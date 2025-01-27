using System.Collections.Generic;

namespace OpenRpg.Core.Variables
{
    public class Variables<T> : KeyedVariables<int, T>, IVariables<T>
    {
        public Variables(int variableType = 0, IDictionary<int, T> internalVariables = null) : base(variableType, internalVariables)
        { }
    }
}
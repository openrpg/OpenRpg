using System.Collections.Generic;

namespace OpenRpg.Core.Variables
{
    public interface IVariables<T>
    {
        void RemoveVariable(int key);
        bool HasVariable(int key);
        T this[int index] { get; set; }
    }
}
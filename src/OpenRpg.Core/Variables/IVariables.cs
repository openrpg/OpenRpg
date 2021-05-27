using System.Collections.Generic;

namespace OpenRpg.Core.Variables
{
    public delegate void VariableChangedEventHandler<T>(object sender, VariableChangedEventArgs<T> e);
    
    public interface IVariables<T> : IEnumerable<KeyValuePair<int, T>>
    {
        event VariableChangedEventHandler<T> OnVariableChanged;
        
        void RemoveVariable(int key);
        bool HasVariable(int key);
        T this[int index] { get; set; }
    }
}
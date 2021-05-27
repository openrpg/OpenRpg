using System.Collections.Generic;

namespace OpenRpg.Core.Variables
{
    public delegate void VariableChangedEventHandler<K,T>(object sender, VariableChangedEventArgs<K,T> e) where K : struct;

    public interface IKeyedVariables<K, T> : IEnumerable<KeyValuePair<K, T>> where K : struct
    {
        event VariableChangedEventHandler<K,T> OnVariableChanged;
        
        void RemoveVariable(K key);
        bool HasVariable(K key);
        T this[K index] { get; set; }
    }
}
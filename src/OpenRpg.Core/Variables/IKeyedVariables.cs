using System.Collections.Generic;

namespace OpenRpg.Core.Variables
{
    public delegate void VariableChangedEventHandler<K,T>(object sender, VariableEventArgs<K,T> e) where K : struct;

    public interface IKeyedVariables<K, T> : IReadOnlyDictionary<K, T> where K : struct
    {
        event VariableChangedEventHandler<K,T> OnVariableChanged;
        event VariableChangedEventHandler<K,T> OnVariableAdded;
        event VariableChangedEventHandler<K,T> OnVariableRemoved;
        
        void RemoveVariable(K key);
        new T this[K index] { get; set; }
    }
}
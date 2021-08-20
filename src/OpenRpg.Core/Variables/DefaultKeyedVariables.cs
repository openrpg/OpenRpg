using System.Collections;
using System.Collections.Generic;

namespace OpenRpg.Core.Variables
{
    public class DefaultKeyedVariables<K,T> : IKeyedVariables<K,T> where K : struct
    {
        public IDictionary<K, T> InternalVariables { get; set; }

        public DefaultKeyedVariables(IDictionary<K, T> internalVariables = null)
        { InternalVariables = internalVariables ?? new Dictionary<K, T>(); }

        public event VariableChangedEventHandler<K,T> OnChanged;
        public event VariableChangedEventHandler<K,T> OnAdded;
        public event VariableChangedEventHandler<K,T> OnRemoved;

        public bool ContainsKey(K key) => InternalVariables.ContainsKey(key);
        public int Count => InternalVariables.Count;
        
        public bool TryGetValue(K key, out T value) => InternalVariables.TryGetValue(key, out value);

        public T this[K index]
        {
            get => InternalVariables[index];
            set
            {
                var containsKey = InternalVariables.ContainsKey(index);
                var oldValue = containsKey ? InternalVariables[index] : default(T);
                InternalVariables[index] = value;
                
                if(containsKey)
                { OnChanged?.Invoke(this, new VariableEventArgs<K,T>(index, oldValue, value)); }
                else
                { OnAdded?.Invoke(this, new VariableEventArgs<K, T>(index, oldValue, value));}
            }
        }

        public T Get(K key) => ContainsKey(key) ? InternalVariables[key] : default(T);
        
        public void AddVariable(K key, T value)
        {
            InternalVariables.Add(key, value);
            OnAdded?.Invoke(this, new VariableEventArgs<K, T>(key, default(T), value));
        }

        public void Remove(K key)
        {
            var value = InternalVariables[key];
            InternalVariables.Remove(key);
            OnRemoved?.Invoke(this, new VariableEventArgs<K, T>(key, value, default(T)));
        }

        public IEnumerable<K> Keys => InternalVariables.Keys;
        public IEnumerable<T> Values => InternalVariables.Values;

        public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
        { return InternalVariables.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }

    }
}
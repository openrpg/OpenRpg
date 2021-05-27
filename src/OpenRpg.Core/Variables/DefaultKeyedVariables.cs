using System.Collections;
using System.Collections.Generic;

namespace OpenRpg.Core.Variables
{
    public class DefaultKeyedVariables<K,T> : IKeyedVariables<K,T> where K : struct
    {
        public IDictionary<K, T> InternalVariables { get; set; } = new Dictionary<K, T>();
        
        public event VariableChangedEventHandler<K,T> OnVariableChanged;

        public T GetVariable(K key) => InternalVariables[key];
        public void AddVariable(K key, T value) => InternalVariables.Add(key, value);
        public void RemoveVariable(K key) => InternalVariables.Remove(key);
        public bool HasVariable(K key) => InternalVariables.ContainsKey(key);

        public T this[K index]
        {
            get => InternalVariables[index];
            set
            {
                var oldValue = InternalVariables.ContainsKey(index) ? InternalVariables[index] : default(T);
                InternalVariables[index] = value;
                OnVariableChanged?.Invoke(this, new VariableChangedEventArgs<K,T>(index, oldValue, value));
            }
        }

        public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
        { return InternalVariables.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}
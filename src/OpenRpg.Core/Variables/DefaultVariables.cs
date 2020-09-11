using System.Collections.Generic;

namespace OpenRpg.Core.Variables
{
    public class DefaultVariables<T> : IVariables<T>
    {
        public IDictionary<int, T> InternalVariables { get; set; } = new Dictionary<int, T>();
        
        public T GetVariable(int key) => InternalVariables[key];
        public void AddVariable(int key, T value) => InternalVariables.Add(key, value);
        public void RemoveVariable(int key) => InternalVariables.Remove(key);
        public bool HasVariable(int key) => InternalVariables.ContainsKey(key);

        public T this[int index]
        {
            get => InternalVariables[index];
            set => InternalVariables[index] = value;
        }
    }
}
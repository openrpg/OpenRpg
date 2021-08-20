using System.Collections.Generic;

namespace OpenRpg.Core.Variables
{
    public delegate void VariableChangedEventHandler<K,T>(object sender, VariableEventArgs<K,T> e) where K : struct;

    public interface IKeyedVariables<K, T> : IReadOnlyDictionary<K, T> where K : struct
    {
        /// <summary>
        /// This is triggered when a value is changed
        /// </summary>
        event VariableChangedEventHandler<K,T> OnChanged;
        
        /// <summary>
        /// This is triggered when a value is added
        /// </summary>
        event VariableChangedEventHandler<K,T> OnAdded;
        
        /// <summary>
        /// This is added when a value is removed
        /// </summary>
        event VariableChangedEventHandler<K,T> OnRemoved;

        /// <summary>
        /// This will do a safe get of the value and will always return something even if the key does not exist
        /// </summary>
        /// <param name="key">The variable key to get</param>
        /// <returns>The value within the variable or a default implementation of T</returns>
        T Get(K key);
        
        /// <summary>
        /// Removes the variable if it exists
        /// </summary>
        /// <param name="key">The key to remove</param>
        void Remove(K key);
        
        /// <summary>
        /// The indexers provide the fastest lookup but is unsafe, so if the variable does not exist it will throw an exception
        /// </summary>
        /// <param name="index">The key to lookup</param>
        new T this[K key] { get; set; }
    }
}
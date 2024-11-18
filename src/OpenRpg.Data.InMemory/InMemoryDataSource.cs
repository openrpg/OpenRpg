using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenRpg.Data.InMemory
{
    public class InMemoryDataSource : IInMemoryDataSource
    {
        /// <summary>
        /// Read only for consumers but can be set directly in inheritance scenarios
        /// </summary>
        public Dictionary<Type, Dictionary<object, object>> Database { get; protected set; }
        
        public object NativeSource => Database;

        public InMemoryDataSource(Dictionary<Type, Dictionary<object, object>> database = null)
        { Database = database ?? new Dictionary<Type, Dictionary<object, object>>(); }
        
        /// <summary>
        /// This constructor is for manual setting in an inherited scenario
        /// </summary>
        protected InMemoryDataSource()
        {}

        public T Get<T>(object id) => (T)Database[typeof(T)][id];
        public IEnumerable<T> GetAll<T>() => Database[typeof(T)].Values.Cast<T>();
        public void Update<T>(T data, object id) => Database[typeof(T)][id] = data;
        public bool Delete<T>(object id) => Database[typeof(T)].Remove(id);
        public bool Exists<T>(object id) => Database[typeof(T)].ContainsKey(id);
        
        public void Create<T>(T data, object id = null)
        {
            if(id == null) { throw new ArgumentNullException(nameof(id), "In Memory DB Requires explicit keys on creation"); }
            Database[typeof(T)].Add(id, data);
        }

        public void Dispose()
        {}
    }
}
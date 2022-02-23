using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenRpg.Data.InMemory
{
    public class InMemoryDataSource : IInMemoryDataSource
    {
        public Dictionary<Type, Dictionary<object, object>> Database { get; }
        
        public object NativeSource => Database;

        public InMemoryDataSource(Dictionary<Type, Dictionary<object, object>> database = null)
        { Database = database ?? new Dictionary<Type, Dictionary<object, object>>(); }

        public T Get<T>(object key) => (T)Database[typeof(T)][key];

        public IEnumerable<T> GetAll<T>() => Database[typeof(T)].Values.Cast<T>();

        public void Create<T>(T data, object key = null)
        {
            if(key == null) { throw new ArgumentNullException(nameof(key), "In Memory DB Requires explicit keys on creation"); }
            Database[typeof(T)].Add(key, data);
        }

        public void Update<T>(T data, object key) => Database[typeof(T)][key] = data;

        public bool Delete<T>(object key) => Database[typeof(T)].Remove(key);

        public bool Exists<T>(object key) => Database[typeof(T)].ContainsKey(key);
    }
}
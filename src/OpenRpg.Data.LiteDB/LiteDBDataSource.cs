using System;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using OpenRpg.Data.LiteDB.Collections;

namespace OpenRpg.Data.LiteDB
{
    public class LiteDBDataSource : ILiteDBDataSource
    {
        /// <summary>
        /// Read only for consumers but can be set directly in inheritance scenarios
        /// </summary>
        public ILiteDatabase Connection { get; protected set; }
        public IDictionary<Type, string> CollectionMappings { get; }
        public IDictionary<Type, ILiteCollectionCache> CollectionCaches { get; } = new Dictionary<Type, ILiteCollectionCache>();

        public object NativeSource => Connection;

        public LiteDBDataSource(ILiteDatabase connection, IDictionary<Type, string> collectionMappings)
        {
            Connection = connection;
            CollectionMappings = collectionMappings;
        }

        public ILiteCollectionCache GetCollectionCacheFor<T>()
        {
            var type = typeof(T);
            if (CollectionCaches.ContainsKey(type))
            { return CollectionCaches[type]; }

            if (!CollectionMappings.ContainsKey(type))
            { throw new Exception($"No mapping available for collection {type.Name}"); }

            var collectionName = CollectionMappings[type];
            var collection = Connection.GetCollection<T>(collectionName);
            var collectionCache = new LiteCollectionCache<T>(collection);
        
            CollectionCaches.Add(type, collectionCache);
            return collectionCache;
        }

        public T Get<T>(object id) => (T)GetCollectionCacheFor<T>().Get(id);
        public IEnumerable<T> GetAll<T>() => GetCollectionCacheFor<T>().GetAll().Cast<T>();
        public void Create<T>(T data, object id = null) => GetCollectionCacheFor<T>().Create(data, id);
        public void Update<T>(T data, object id) => GetCollectionCacheFor<T>().Update(data, id);
        public bool Delete<T>(object id) => GetCollectionCacheFor<T>().Delete(id);
        public bool Exists<T>(object id) => GetCollectionCacheFor<T>().Exists(id);

        public void Dispose()
        { Connection?.Dispose(); }
    }
}
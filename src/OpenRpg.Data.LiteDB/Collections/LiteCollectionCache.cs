using System.Collections;
using LiteDB;

namespace OpenRpg.Data.LiteDB.Collections
{
    public class LiteCollectionCache<T> : ILiteCollectionCache
    {
        public ILiteCollection<T> Collection { get; }

        public LiteCollectionCache(ILiteCollection<T> collection)
        { Collection = collection; }

        public IEnumerable GetAll() => Collection.FindAll();
        public object Get(object id) => Collection.FindById(new BsonValue(id));
        public void Create(object data, object id = null) => Collection.Insert(new BsonValue(id), (T)data);
        public void Update(object data, object id) => Collection.Update(new BsonValue(id), (T)data);
        public bool Delete(object id) => Collection.Delete(new BsonValue(id));
        public bool Exists(object id) => Collection.Exists(new BsonValue(id));
    }
}
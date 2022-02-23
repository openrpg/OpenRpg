using System.Collections.Generic;
using System.Data;
using Dapper;

namespace OpenRpg.Data.Database
{
    public class DatabaseDataSource : IDatabaseDataSource
    {
        public IDbConnection Connection { get; }
        
        public object NativeSource => Connection;

        public DatabaseDataSource(IDbConnection connection)
        { Connection = connection; }

        public T Get<T>(object key) => Connection.Get<T>(key);
        public IEnumerable<T> GetAll<T>() => Connection.GetList<T>();
        public void Create<T>(T data, object key = null) => Connection.Insert(data);
        public void Update<T>(T data, object key) => Connection.Update(data);
        public bool Delete<T>(object key) => Connection.Delete<T>(key) > 0;
        public bool Exists<T>(object key) => Get<T>(key) != null;
    }
}
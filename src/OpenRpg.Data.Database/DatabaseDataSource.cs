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

        public T Get<T>(object id) => Connection.Get<T>(id);
        public IEnumerable<T> GetAll<T>() => Connection.GetList<T>();
        public void Create<T>(T data, object id = null) => Connection.Insert(data);
        public void Update<T>(T data, object id) => Connection.Update(data);
        public bool Delete<T>(object id) => Connection.Delete<T>(id) > 0;
        public bool Exists<T>(object id) => Get<T>(id) != null;
    }
}
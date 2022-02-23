using System.Collections.Generic;

namespace OpenRpg.Data
{
    public interface IDataSource
    {
        object NativeSource { get; }
        
        T Get<T>(object key); 
        IEnumerable<T> GetAll<T>();
        void Create<T>(T data, object key = null);
        void Update<T>(T data, object key);
        bool Delete<T>(object key);
        bool Exists<T>(object key);
    }
}
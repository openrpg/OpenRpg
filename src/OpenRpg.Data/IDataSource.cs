using System;
using System.Collections.Generic;

namespace OpenRpg.Data
{
    public interface IDataSource : IDisposable
    {
        object NativeSource { get; }
        
        T Get<T>(object id); 
        IEnumerable<T> GetAll<T>();
        void Create<T>(T data, object id = null);
        void Update<T>(T data, object id);
        bool Delete<T>(object id);
        bool Exists<T>(object id);
    }
}
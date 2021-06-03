using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Data.Queries;
using OpenRpg.Data.Repositories;

namespace OpenRpg.Data.Defaults
{
    /// <summary>
    /// A simple implementation of a repository 
    /// </summary>
    /// <remarks>
    /// It is expected that you would provide a populated enumerable to the constructor for most use cases here as the
    /// data source, however you can extend it and built up the data internally if required for hard coded scenarios.
    /// </remarks>
    /// <typeparam name="T">Entity type</typeparam>
    public abstract class InMemoryRepository<T,K> : IRepository<T,K>
    {
        public List<T> Data { get; protected set; }

        public InMemoryRepository(IEnumerable<T> data)
        { Data = data.ToList(); }
        
        protected InMemoryRepository() 
        { Data = new List<T>(); }

        protected abstract K GetKeyFromEntity(T entity);
        
        public void Create(T entry) =>  Data.Add(entry);
        public T Retrieve(K id) => Data.SingleOrDefault(x => GetKeyFromEntity(x).Equals(id));
        public void Update(T entry) {}
        public void Delete(T entry) => Data.Remove(entry);
        
        public IEnumerable<T> FindAll(IFindAllQuery<T> dataQuery) => dataQuery.Execute(Data);
        public T2 Find<T2>(IFindQuery<T2> dataQuery) => dataQuery.Execute(Data);
        public object Execute(IExecuteQuery query) => query.Execute(Data);
    }
}
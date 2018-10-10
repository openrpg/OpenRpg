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
    /// <typeparam name="T">Entity type</typeparam>
    public abstract class InMemoryRepository<T,K> : IRepository<T,K>
    {
        public List<T> Data { get; }

        public InMemoryRepository(IEnumerable<T> data)
        { Data = data.ToList(); }

        protected abstract K GetKeyFromEntity(T entity);
        
        public T Retrieve(K id) => Data.SingleOrDefault(x => GetKeyFromEntity(x).Equals(id));
        
        public IEnumerable<T> Find(IFindQuery<T> dataQuery) => dataQuery.Find(Data);
        public IEnumerable<T2> Find<T2>(IFindQuery<T2> dataQuery) => dataQuery.Find(Data);

        public void Create(T entry) =>  Data.Add(entry);

        public void Update(T entry) {}

        public void Delete(T entry) => Data.Remove(entry);
    }
}
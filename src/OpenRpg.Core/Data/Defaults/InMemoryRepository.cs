using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Common;
using OpenRpg.Core.Data.Queries;
using OpenRpg.Core.Data.Repositories;

namespace OpenRpg.Core.Data.Defaults
{
    /// <summary>
    /// A simple implementation of a repository 
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class InMemoryRepository<T> : IRepository<T> where T : IHasDataId
    {
        public List<T> Data { get; }

        public InMemoryRepository(IEnumerable<T> data)
        { Data = data.ToList(); }

        public T Retrieve(int id) => Data.SingleOrDefault(x => x.Id == id);
        
        public IEnumerable<T> Find(IFindQuery<T> query) => query.Find(Data);

        public IEnumerable<CustomT> Find<CustomT>(IFindQuery<CustomT> query) where CustomT : IHasDataId
        { return query.Find(Data); }

        public void Create(T entry) => Data.Add(entry);

        public void Update(T entry) {}

        public void Delete(T entry) => Data.Remove(entry);
    }
}
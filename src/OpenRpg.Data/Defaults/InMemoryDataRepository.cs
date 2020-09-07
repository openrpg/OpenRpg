using System.Collections.Generic;
using OpenRpg.Core.Common;

namespace OpenRpg.Data.Defaults
{
    /// <summary>
    /// This is a layer on top of the InMemoryRepository that has awareness of the IHasDataId for Id lookup purposes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InMemoryDataRepository<T> : InMemoryRepository<T, int> where T : IHasDataId
    {
        protected override int GetKeyFromEntity(T entity) => entity.Id;
        
        public InMemoryDataRepository(IEnumerable<T> data) : base(data)
        {}
        
        protected InMemoryDataRepository()
        {}
    }
}
using System.Collections.Generic;
using OpenRpg.Core.Common;

namespace OpenRpg.Data.Defaults
{
    public abstract class InMemoryDataRepository<T> : InMemoryRepository<T, int> where T : IHasDataId
    {
        protected override int GetKeyFromEntity(T entity) => entity.Id;
        
        protected InMemoryDataRepository(IEnumerable<T> data) : base(data)
        {}
    }
}
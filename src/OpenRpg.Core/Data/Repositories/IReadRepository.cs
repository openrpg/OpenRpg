using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Data.Queries;

namespace OpenRpg.Core.Data.Repositories
{
    public interface IReadRepository<T> where T : IHasDataId
    {
        T Retrieve(int id);

        IEnumerable<T> Find(IFindQuery<T> query);
        IEnumerable<CustomT> Find<CustomT>(IFindQuery<CustomT> query) where CustomT : IHasDataId;
    }
}
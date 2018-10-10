using System.Collections.Generic;
using OpenRpg.Data.Queries;

namespace OpenRpg.Data.Repositories
{
    public interface IReadRepository<T, in K>
    {
        T Retrieve(K id);

        IEnumerable<T> Find(IFindQuery<T> query);
        IEnumerable<T2> Find<T2>(IFindQuery<T2> query);
    }
}
using System.Collections.Generic;

namespace OpenRpg.Data.Queries
{
    public interface IFindQuery<out T>
    {
        IEnumerable<T> Find(object dataSource);
    }
}
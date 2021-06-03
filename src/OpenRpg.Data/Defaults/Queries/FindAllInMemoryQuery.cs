using System.Collections.Generic;
using OpenRpg.Data.Queries;

namespace OpenRpg.Data.Defaults.Queries
{
    public abstract class FindAllInMemoryQuery<T> : IFindAllQuery<T>
    {
        public IEnumerable<T> Execute(object dataSource)
        { return Execute((List<T>) dataSource); }

        public abstract IEnumerable<T> Execute(List<T> dataSource);
    }
}
using System.Collections.Generic;
using OpenRpg.Data.Queries;

namespace OpenRpg.Data.Defaults.Queries
{
    public abstract class FindInMemoryQuery<T, T2> : IFindQuery<T>
    {
        public T Execute(object dataSource)
        { return Execute((List<T2>) dataSource); }

        public abstract T Execute(List<T2> dataSource);
    }
}
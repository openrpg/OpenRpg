using System.Collections.Generic;
using OpenRpg.Data.Queries;

namespace OpenRpg.Data.Defaults
{
    public abstract class InMemoryFindQuery<T> : IFindQuery<T>
    {
        public IEnumerable<T> Find(object dataSource) => Find(dataSource as List<T>);

        protected abstract IEnumerable<T> Find(List<T> dataSource);
    }
}
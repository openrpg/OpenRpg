using System.Collections.Generic;
using OpenRpg.Core.Common;
using OpenRpg.Core.Data.Queries;

namespace OpenRpg.Core.Data.Defaults
{
    public abstract class InMemoryFindQuery<T> : IFindQuery<T> where T : IHasDataId
    {
        public IEnumerable<T> Find(object dataSource) => Find(dataSource as List<T>);

        protected abstract IEnumerable<T> Find(List<T> dataSource);
    }
}
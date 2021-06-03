using System.Collections.Generic;
using OpenRpg.Data.Queries;

namespace OpenRpg.Data.Defaults.Queries
{
    public abstract class ExecuteInMemoryQuery<T> : IExecuteQuery
    {
        public object Execute(object dataSource)
        { return Execute((List<T>) dataSource); }

        public abstract object Execute(List<T> dataSource);
    }
}
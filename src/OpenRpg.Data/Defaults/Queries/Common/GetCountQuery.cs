using System.Collections.Generic;

namespace OpenRpg.Data.Defaults.Queries.Common
{
    public class GetCountQuery<T> : FindInMemoryQuery<int, T>
    {
        public override int Execute(List<T> dataSource)
        { return dataSource.Count; }
    }
}
using System.Collections.Generic;

namespace OpenRpg.Data.Defaults.Queries.Common
{
    public class GetAllQuery<T> : FindAllInMemoryQuery<T>
    {
        public override IEnumerable<T> Execute(List<T> dataSource)
        { return dataSource; }
    }
}
using System.Collections.Generic;

namespace OpenRpg.Data.Conventions.Queries
{
    public class GetAllEntityQuery<T> : IFindQuery<T>
    {
        public IEnumerable<T> Execute(IDataSource dataSource)
        { return dataSource.GetAll<T>(); }
    }
}
using System;
using System.Collections.Generic;

namespace OpenRpg.Data.Defaults.Queries.Common
{
    public class DynamicGetAllQuery<T> : FindAllInMemoryQuery<T>
    {
        public Func<IEnumerable<T>,IEnumerable<T>> Filter { get; }

        public DynamicGetAllQuery(Func<IEnumerable<T>,IEnumerable<T>> filter)
        { Filter = filter; }

        public override IEnumerable<T> Execute(List<T> dataSource)
        { return Filter(dataSource); }
    }
}
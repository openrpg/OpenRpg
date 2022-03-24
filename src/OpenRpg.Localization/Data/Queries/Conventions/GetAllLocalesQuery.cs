using System.Collections.Generic;
using System.Linq;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Localization.Data.Queries.Conventions
{
    public class GetAllLocalesQuery : ILocaleQuery<IEnumerable<string>>
    {
        public string[] Ids { get; }

        public GetAllLocalesQuery(params string[] ids)
        { Ids = ids; }

        public IEnumerable<string> Execute(string locale, ILocaleDataSource dataSource)
        { return Ids.Select(x => dataSource.Get(locale, x)); }
    }
}
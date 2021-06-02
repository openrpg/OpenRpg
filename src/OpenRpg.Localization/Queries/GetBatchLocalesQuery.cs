using System.Collections.Generic;
using OpenRpg.Data.Queries;

namespace OpenRpg.Localization.Queries
{
    public class GetBatchLocalesQuery : IFindQuery<string>
    {
        public IReadOnlyList<string> Ids { get;}

        public GetBatchLocalesQuery(IReadOnlyList<string> ids)
        { Ids = ids; }

        public IEnumerable<string> Find(object dataSource)
        {
            var localeStore = dataSource as LocaleDataset;
            
            for (var i = 0; i < Ids.Count; i++)
            { yield return localeStore.LocaleData[Ids[i]]; }
        }
    }
}
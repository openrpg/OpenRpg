using System.Collections.Generic;
using OpenRpg.Data.Queries;

namespace OpenRpg.Localization.Repositories
{
    public class LocaleRepository : ILocaleRepository 
    {
        public string LocaleCode { get; set; }
        public LocaleDatastore LocaleDatastore { get; set; } = new LocaleDatastore();
        
        public string Retrieve(string id)
        { return LocaleDatastore[id]; }

        public IEnumerable<string> Find(IFindQuery<string> query)
        { return query.Find(LocaleDatastore); }

        public IEnumerable<T2> Find<T2>(IFindQuery<T2> query)
        { return query.Find(LocaleDatastore); }

        public void Create(string id, string text)
        { LocaleDatastore.Add(id, text); }

        public void Update(string id, string text)
        { LocaleDatastore[id] = text; }

        public void Delete(string id)
        { LocaleDatastore.Remove(id); }
    }
}
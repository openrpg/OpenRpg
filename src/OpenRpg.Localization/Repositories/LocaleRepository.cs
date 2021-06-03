using System.Collections.Generic;
using OpenRpg.Data.Queries;

namespace OpenRpg.Localization.Repositories
{
    public class LocaleRepository : ILocaleRepository
    {
        public string LocaleCode => LocaleDataset.LocaleCode;
        public LocaleDataset LocaleDataset { get; protected set; }

        public LocaleRepository(LocaleDataset localeDataset)
        { LocaleDataset = localeDataset; }

        public string Retrieve(string id)
        { return LocaleDataset.LocaleData[id]; }

        public void ChangeLocale(LocaleDataset dataset)
        { LocaleDataset = dataset; }

        public bool Has(string id)
        { return LocaleDataset.LocaleData.ContainsKey(id); }

        public IEnumerable<string> FindAll(IFindAllQuery<string> query)
        { return query.Execute(LocaleDataset); }

        public T2 Find<T2>(IFindQuery<T2> query)
        { return query.Execute(LocaleDataset); }

        public void Create(string id, string text)
        { LocaleDataset.LocaleData.Add(id, text); }

        public void Update(string id, string text)
        { LocaleDataset.LocaleData[id] = text; }

        public void Delete(string id)
        { LocaleDataset.LocaleData.Remove(id); }
    }
}
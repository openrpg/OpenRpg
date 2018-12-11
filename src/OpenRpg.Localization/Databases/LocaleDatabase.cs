using System.Collections.Generic;
using OpenRpg.Localization.Repositories;

namespace OpenRpg.Localization.Databases
{
    public class LocaleDatabase : ILocaleDatabase
    {
        public IDictionary<string, ILocaleRepository> LocaleDatasets { get; set; } = new Dictionary<string, ILocaleRepository>();

        public ILocaleRepository GetRepository(string localeCode)
        { 
            if (!LocaleDatasets.ContainsKey(localeCode))
            { return null; }

            return LocaleDatasets[localeCode];
        }

        public void AddRepository(ILocaleRepository repository)
        { LocaleDatasets.Add(repository.LocaleCode, repository); }

        public void RemoveRepository(string localeCode)
        { LocaleDatasets.Remove(localeCode); }

        public IEnumerable<ILocaleRepository> GetAllRepositories()
        { return LocaleDatasets.Values; }
    }
}
using System.Collections.Generic;

namespace OpenRpg.Localization
{
    public class LocaleDatabase : ILocaleDatabase
    {
        public IDictionary<string, ILocaleDataset> LocaleDatasets { get; set; }
        public string LocaleCode { get; set; }

        public ILocaleDataset GetDataset(string localeCode)
        { 
            if (!LocaleDatasets.ContainsKey(localeCode))
            { return null; }

            return LocaleDatasets[localeCode];
        }

        public void AddDataset(ILocaleDataset dataset)
        { LocaleDatasets.Add(dataset.LocaleCode, dataset); }

        public void RemoveDataset(string localeCode)
        { LocaleDatasets.Remove(localeCode); }
    }
}
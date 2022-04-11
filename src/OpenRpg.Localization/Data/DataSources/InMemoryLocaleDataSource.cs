using System.Collections.Generic;
using System.Linq;

namespace OpenRpg.Localization.Data.DataSources
{
    public class InMemoryLocaleDataSource : ILocaleDataSource
    {
        /// <summary>
        /// Readonly for consumers but protected for inheritance scenarios to directly work with 
        /// </summary>
        public Dictionary<string, LocaleDataset> LocaleDatasets { get; protected set; }
        
        public object NativeSource => LocaleDatasets;

        public InMemoryLocaleDataSource(IEnumerable<LocaleDataset> localeDatasets = null)
        {
            LocaleDatasets = localeDatasets?.ToDictionary(x => x.LocaleCode, x => x) ?? 
                             new Dictionary<string, LocaleDataset>();
        }
        
        /// <summary>
        /// For usage in inheritance scenarios where you internally set data
        /// </summary>
        protected InMemoryLocaleDataSource()
        {}

        public string Get(string localeCode, string key) => LocaleDatasets[localeCode].LocaleData[key];
        public void Create(string localeCode, string text, string key) => LocaleDatasets[localeCode].LocaleData.Add(key, text);
        public void Update(string localeCode, string text, string key) => LocaleDatasets[localeCode].LocaleData[key] = text;
        public bool Delete(string localeCode, string key) => LocaleDatasets[localeCode].LocaleData.Remove(key);
        public bool Exists(string localeCode, string key) => LocaleDatasets[localeCode].LocaleData.ContainsKey(key);
        public LocaleDataset GetLocaleDataset(string localeCode) => LocaleDatasets[localeCode];
    }
}
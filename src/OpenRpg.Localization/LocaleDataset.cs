using System.Collections.Generic;

namespace OpenRpg.Localization
{
    public class LocaleDataset : ILocaleDataset 
    {
        public string LocaleCode { get; set; }
        public IDictionary<string, string> LocaleDetails { get; set; } = new Dictionary<string, string>();
        
        public string GetById(string localeId)
        {
            if (!LocaleDetails.ContainsKey(localeId))
            { return null; }
            
            return LocaleDetails[localeId];
        }

        public IEnumerable<string> GetAll(params string[] localeIds)
        {
            for (var i = 0; i < localeIds.Length; i++)
            { yield return GetById(localeIds[i]); }
        }
    }
}
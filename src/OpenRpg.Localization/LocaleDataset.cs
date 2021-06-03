using System.Collections.Generic;

namespace OpenRpg.Localization
{
    public class LocaleDataset
    {
        public string LocaleCode { get; set; }
        public Dictionary<string, string> LocaleData { get; set; } = new Dictionary<string, string>();
    }
}
using System.Collections.Generic;

namespace OpenRpg.Localization
{
    public interface ILocaleDataset
    {
        string LocaleCode { get; }
        string GetById(string localeId);
        IEnumerable<string> GetAll(params string[] localeIds);
    }
}
using System.Collections.Generic;
using OpenRpg.Editor.Infrastructure.Models;

namespace OpenRpg.Editor.Infrastructure.Services
{
    public interface ILocaleComparisonService
    {
        List<CrossLocaleEntry> GetCrossLocaleTable();
        HashSet<string> GetAllLocaleIds();
        List<CrossLocaleEntry> GetMissingTranslations(string sourceLocaleCode);
        void CopyToLocale(string sourceLocaleCode, string targetLocaleCode, IEnumerable<string> localeIds);
        void FillAllMissing(string sourceLocaleCode, string targetLocaleCode);
        LocaleEntry GetEntryFor(string localeCode, string localeId);
        void SaveEntry(LocaleEntry entry);
    }
}

using System.Collections.Generic;
using System.Linq;
using OpenRpg.Editor.Infrastructure.Models;
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Editor.Infrastructure.Services
{
    public class LocaleComparisonService : ILocaleComparisonService
    {
        private readonly ILocaleDataSource _dataSource;

        public LocaleComparisonService(ILocaleDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public HashSet<string> GetAllLocaleIds()
        {
            var allIds = new HashSet<string>();
            foreach (var localeCode in _dataSource.GetLocaleCodes())
            {
                var dataset = _dataSource.GetLocaleDataset(localeCode);
                if (dataset?.LocaleData == null) continue;
                foreach (var key in dataset.LocaleData.Keys)
                { allIds.Add(key); }
            }
            return allIds;
        }

        public List<CrossLocaleEntry> GetCrossLocaleTable()
        {
            var allIds = GetAllLocaleIds();
            var localeCodes = _dataSource.GetLocaleCodes().OrderBy(x => x).ToList();

            return allIds.OrderBy(x => x).Select(id =>
            {
                var entries = localeCodes.Select(lc =>
                {
                    var dataset = _dataSource.GetLocaleDataset(lc);
                    var exists = dataset?.LocaleData?.ContainsKey(id) == true;
                    return new LocaleEntry
                    {
                        LocaleCode = lc,
                        LocaleId = id,
                        LocaleText = exists ? dataset.LocaleData[id] : string.Empty,
                        Exists = exists
                    };
                }).ToList();

                return new CrossLocaleEntry { LocaleId = id, Entries = entries };
            }).ToList();
        }

        public List<CrossLocaleEntry> GetMissingTranslations(string sourceLocaleCode)
        {
            var localeCodes = _dataSource.GetLocaleCodes().ToList();
            if (!localeCodes.Contains(sourceLocaleCode))
                return new List<CrossLocaleEntry>();

            var sourceDataset = _dataSource.GetLocaleDataset(sourceLocaleCode);
            if (sourceDataset?.LocaleData == null)
                return new List<CrossLocaleEntry>();

            var results = new List<CrossLocaleEntry>();
            foreach (var kvp in sourceDataset.LocaleData)
            {
                var missingEntries = localeCodes
                    .Where(lc => lc != sourceLocaleCode)
                    .Select(lc =>
                    {
                        var dataset = _dataSource.GetLocaleDataset(lc);
                        var exists = dataset?.LocaleData?.ContainsKey(kvp.Key) == true;
                        return new LocaleEntry
                        {
                            LocaleCode = lc,
                            LocaleId = kvp.Key,
                            LocaleText = exists ? dataset.LocaleData[kvp.Key] : string.Empty,
                            Exists = exists
                        };
                    })
                    .Where(e => !e.Exists)
                    .ToList();

                if (missingEntries.Any())
                {
                    var allEntries = localeCodes.Select(lc =>
                    {
                        var dataset = _dataSource.GetLocaleDataset(lc);
                        var exists = dataset?.LocaleData?.ContainsKey(kvp.Key) == true;
                        return new LocaleEntry
                        {
                            LocaleCode = lc,
                            LocaleId = kvp.Key,
                            LocaleText = exists ? dataset.LocaleData[kvp.Key] : string.Empty,
                            Exists = exists
                        };
                    }).ToList();

                    results.Add(new CrossLocaleEntry { LocaleId = kvp.Key, Entries = allEntries });
                }
            }

            return results;
        }

        public void CopyToLocale(string sourceLocaleCode, string targetLocaleCode, IEnumerable<string> localeIds)
        {
            var sourceDataset = _dataSource.GetLocaleDataset(sourceLocaleCode);
            if (sourceDataset?.LocaleData == null) return;

            foreach (var id in localeIds)
            {
                if (sourceDataset.LocaleData.TryGetValue(id, out var text))
                {
                    if (!_dataSource.Exists(targetLocaleCode, id))
                    { _dataSource.Create(targetLocaleCode, id, text); }
                }
            }
        }

        public void FillAllMissing(string sourceLocaleCode, string targetLocaleCode)
        {
            var sourceDataset = _dataSource.GetLocaleDataset(sourceLocaleCode);
            var targetDataset = _dataSource.GetLocaleDataset(targetLocaleCode);
            if (sourceDataset?.LocaleData == null || targetDataset?.LocaleData == null) return;

            foreach (var kvp in sourceDataset.LocaleData)
            {
                if (!targetDataset.LocaleData.ContainsKey(kvp.Key))
                { _dataSource.Create(targetLocaleCode, kvp.Key, kvp.Value); }
            }
        }

        public LocaleEntry GetEntryFor(string localeCode, string localeId)
        {
            var exists = _dataSource.Exists(localeCode, localeId);
            return new LocaleEntry
            {
                LocaleCode = localeCode,
                LocaleId = localeId,
                LocaleText = exists ? _dataSource.Get(localeCode, localeId) : string.Empty,
                Exists = exists
            };
        }

        public void SaveEntry(LocaleEntry entry)
        {
            if (entry.Exists)
            { _dataSource.Update(entry.LocaleCode, entry.LocaleId, entry.LocaleText); }
            else
            { _dataSource.Create(entry.LocaleCode, entry.LocaleId, entry.LocaleText); }
            entry.Exists = true;
        }
    }
}

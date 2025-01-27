using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Localization.Data.Queries.Conventions
{
    public class GetLocaleDatasetQuery : ILocaleQuery<LocaleDataset>
    {
        public string LocaleOverride { get; set; }

        public GetLocaleDatasetQuery(string localeOverride = null)
        { LocaleOverride = localeOverride; }
        
        public LocaleDataset Execute(string locale, ILocaleDataSource dataSource)
        { return dataSource.GetLocaleDataset(LocaleOverride ?? locale); }
    }
}
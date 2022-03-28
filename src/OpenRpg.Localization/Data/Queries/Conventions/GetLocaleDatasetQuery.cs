using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Localization.Data.Queries.Conventions
{
    public class GetLocaleDatasetQuery : ILocaleQuery<LocaleDataset>
    {
        public string localeOverride { get; set; }

        public GetLocaleDatasetQuery(string localeOverride = null)
        { this.localeOverride = localeOverride; }
        
        public LocaleDataset Execute(string locale, ILocaleDataSource dataSource)
        { return dataSource.GetLocaleDataset(localeOverride ?? locale); }
    }
}
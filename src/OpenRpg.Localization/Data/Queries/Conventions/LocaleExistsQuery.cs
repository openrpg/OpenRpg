using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Localization.Data.Queries.Conventions
{
    public class LocaleExistsQuery : ILocaleQuery<bool>
    {
        public string Id { get; }

        public LocaleExistsQuery(string id)
        { Id = id; }

        public bool Execute(string locale, ILocaleDataSource dataSource)
        { return dataSource.Exists(locale, Id); }
    }
}
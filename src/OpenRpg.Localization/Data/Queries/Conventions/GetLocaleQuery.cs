using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Localization.Data.Queries.Conventions
{
    public class GetLocaleQuery : ILocaleQuery<string>
    {
        public string Id { get; }

        public GetLocaleQuery(string id)
        { Id = id; }

        public string Execute(string locale, ILocaleDataSource dataSource)
        { return dataSource.Get(locale, Id); }
    }
}
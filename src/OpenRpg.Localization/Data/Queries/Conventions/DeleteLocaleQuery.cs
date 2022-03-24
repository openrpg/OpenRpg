using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Localization.Data.Queries.Conventions
{
    public class DeleteLocaleQuery : ILocaleQuery<bool>
    {
        public string Id { get; }

        public DeleteLocaleQuery(string id)
        { Id = id; }

        public bool Execute(string localeCode, ILocaleDataSource dataSource)
        {
            dataSource.Delete(localeCode, Id);
            return true;
        }
    }
}
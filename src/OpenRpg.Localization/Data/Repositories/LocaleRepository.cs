using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Localization.Data.Queries;

namespace OpenRpg.Localization.Data.Repositories
{
    public class LocaleRepository : ILocaleRepository
    {
        public string CurrentLocaleCode { get; private set; }
        public ILocaleDataSource DataSource { get; }

        public LocaleRepository(ILocaleDataSource dataSource, string localeCodeToUse)
        {
            DataSource = dataSource;
            CurrentLocaleCode = localeCodeToUse;
        }

        public void ChangeLocale(string localeCode)
        { CurrentLocaleCode = localeCode; }

        public T Query<T>(ILocaleQuery<T> query) => query.Execute(CurrentLocaleCode, DataSource);
    }
}
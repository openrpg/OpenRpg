using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Localization.Data.Queries;

namespace OpenRpg.Localization.Data.Repositories
{
    public interface ILocaleRepository
    {
        string CurrentLocaleCode { get; }
        void ChangeLocale(string localeCode);

        ILocaleDataSource DataSource { get; }
        
        T Query<T>(ILocaleQuery<T> query);
    }
}
using OpenRpg.Localization.Data.DataSources;

namespace OpenRpg.Localization.Data.Queries
{
    public interface ILocaleQuery<out T>
    {
        T Execute(string locale, ILocaleDataSource dataSource);
    }
}
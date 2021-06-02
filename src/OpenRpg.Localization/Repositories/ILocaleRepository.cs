using OpenRpg.Data.Repositories;

namespace OpenRpg.Localization.Repositories
{
    public interface ILocaleRepository : IReadRepository<string, string>
    {
        string LocaleCode { get; }

        void ChangeLocale(LocaleDataset dataset);
        bool Has(string id);
        void Create(string id, string text);
        void Update(string id, string text);
        void Delete(string id);
    }
}
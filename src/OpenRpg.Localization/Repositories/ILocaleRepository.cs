namespace OpenRpg.Localization.Repositories
{
    public interface ILocaleRepository
    {
        string LocaleCode { get; }

        string Get(string id);
        void ChangeLocale(LocaleDataset dataset);
        bool Has(string id);
        void Create(string id, string text);
        void Update(string id, string text);
        void Delete(string id);
        
        
    }
}
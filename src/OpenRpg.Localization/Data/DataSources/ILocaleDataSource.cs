namespace OpenRpg.Localization.Data.DataSources
{
    public interface ILocaleDataSource
    {
        object NativeSource { get; }
        string Get(string localeCode, string id);
        void Create(string localeCode, string id, string text);
        void Update(string localeCode, string id, string text);
        bool Delete(string localeCode, string id);
        bool Exists(string localeCode, string id);
        LocaleDataset GetLocaleDataset(string localeCode);
    }
}
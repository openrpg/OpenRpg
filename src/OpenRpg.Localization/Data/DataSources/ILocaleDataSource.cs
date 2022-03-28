namespace OpenRpg.Localization.Data.DataSources
{
    public interface ILocaleDataSource
    {
        object NativeSource { get; }
        string Get(string localeCode, string key);
        void Create(string localeCode, string text, string key);
        void Update(string localeCode, string text, string key);
        bool Delete(string localeCode, string key);
        bool Exists(string localeCode, string key);
        LocaleDataset GetLocaleDataset(string localeCode);
    }
}
namespace OpenRpg.Localization
{
    public interface ILocaleDatabase
    {
        ILocaleDataset GetDataset(string localeCode);
        void AddDataset(ILocaleDataset dataset);
        void RemoveDataset(string dataset);
    }
}
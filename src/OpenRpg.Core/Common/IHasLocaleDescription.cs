namespace OpenRpg.Core.Localization
{
    public interface IHasLocaleDescription
    {
        string NameLocaleId { get; }
        string DescriptionLocaleId { get; }
    }
}
namespace OpenRpg.Core.Common
{
    /// <summary>
    /// This implies the implementor has a name and description field that should be locale aware
    /// </summary>
    /// <remarks>
    /// In reality you can populate this with hardcoded strings but it is meant to represent the locale ids for
    /// each entry so it can be looked up within a locale data store/repository (assumed the provided ones).
    /// </remarks>
    public interface IHasLocaleDescription
    {
        /// <summary>
        /// The id of the name locale entry for this entity
        /// </summary>
        string NameLocaleId { get; }
        
        /// <summary>
        /// The id of the description locale entry for this entity
        /// </summary>
        string DescriptionLocaleId { get; }
    }
}
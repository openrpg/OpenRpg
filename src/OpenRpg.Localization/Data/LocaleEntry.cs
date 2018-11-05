namespace OpenRpg.Localization.Data
{
    public class LocaleEntry
    {
        /// <summary>
        /// The locale code the entry applies to, i.e en-us, nl-be etc
        /// </summary>
        public string LocaleCode { get; set; }
        
        /// <summary>
        /// The id of the locale entry
        /// </summary>
        /// <remarks>
        /// The id should be unique, but shared across different locale codes
        /// </remarks>
        public string LocaleId { get; set; }
        
        /// <summary>
        /// The actual text related to this locale
        /// </summary>
        public string LocaleText { get; set; }
    }
}
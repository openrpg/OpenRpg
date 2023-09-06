namespace OpenRpg.Core.Common
{
    /// <summary>
    /// This implies an association with some other data
    /// </summary>
    /// <remarks>
    /// For example a requirement may require a stat value, so the associated Id would be the stat id it depends on
    /// and the associated value would be the minimum required stat value it would allow. Another example could
    /// be for quests where you have a reward which is an associated id for an item id and an associated value for the
    /// amount to receive
    /// </remarks>
    public interface IHasAssociation
    {
        /// <summary>
        /// The required id of whatever is associated
        /// </summary>
        int AssociatedId { get; }
        
        /// <summary>
        /// The required value of whatever is associated (may be a minimum threshold not equality)
        /// </summary>
        int AssociatedValue { get; }
    }
}
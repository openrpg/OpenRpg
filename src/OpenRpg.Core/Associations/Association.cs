namespace OpenRpg.Core.Associations
{
    public struct Association
    {
        /// <summary>
        /// The required id of whatever is associated
        /// </summary>
        public int AssociatedId { get; }
        
        /// <summary>
        /// The required value of whatever is associated (may be a minimum threshold not equality)
        /// </summary>
        public int AssociatedValue { get; }

        public Association(int associatedId, int associatedValue)
        {
            AssociatedId = associatedId;
            AssociatedValue = associatedValue;
        }
    }
}
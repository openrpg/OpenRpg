namespace OpenRpg.Items.Equipment
{
    /// <summary>
    /// The equipment slot wraps up a given slot and what may be in there
    /// </summary>
    public interface IEquipmentSlot
    {
        /// <summary>
        /// Indicates if a given item type can be put into this slot
        /// </summary>
        /// <param name="itemType">The type of item to check against</param>
        /// <returns>True if it can be equipped in this slot, false if not</returns>
        bool CanEquipItemType(int itemType);
        
        /// <summary>
        /// The current item within this slot
        /// </summary>
        IItem SlottedItem { get; }

        /// <summary>
        /// Unequip an item from the slot
        /// </summary>
        /// <returns>Returns the item that was equipped, or null if nothing was equipped</returns>
        IItem UnequipItem();
        
        /// <summary>
        /// Equips and item to the slot, if an item is already in the slot it needs unequipping explicitly first
        /// </summary>
        /// <param name="item">The item to equip</param>
        /// <returns>True if it was empty and can be equipped, false if it cannot be equipped or an item is already equipped</returns>
        bool EquipItemToSlot(IItem item);
    }
}
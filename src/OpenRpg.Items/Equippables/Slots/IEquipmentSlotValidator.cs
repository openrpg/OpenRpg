namespace OpenRpg.Items.Equippables.Slots
{
    /// <summary>
    /// Validates if a given slot can equip a given item
    /// </summary>
    public interface IEquipmentSlotValidator
    {
        /// <summary>
        /// Indicates if a given item type can be put into this slot
        /// </summary>
        /// <param name="slotType">The type of slot</param>
        /// <param name="itemType">The type of item to check against</param>
        /// <returns>True if it can be equipped in this slot, false if not</returns>
        bool CanEquipItemType(int slotType, int itemType);
    }
}
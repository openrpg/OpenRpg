namespace OpenRpg.Items.Equipment
{
    public interface IEquipmentSlot<TItem> where TItem : class, IItem
    {
        int SlotType { get; }
        TItem SlottedItem { get; }

        TItem UnequipItem();
        bool EquipItemToSlot(TItem item);
    }
}
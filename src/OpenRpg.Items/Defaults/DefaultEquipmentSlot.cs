using OpenRpg.Items.Equipment;

namespace OpenRpg.Items.Defaults
{
    public abstract class DefaultEquipmentSlot<TItem> : IEquipmentSlot<TItem> 
        where TItem : class, IItem
    {
        public abstract int SlotType { get; }
        public TItem SlottedItem { get; private set; }

        public TItem UnequipItem()
        {
            if(SlottedItem == null) { return null; }

            var returningItem = SlottedItem;
            SlottedItem = null;

            return returningItem;
        }

        public bool EquipItemToSlot(TItem item)
        {
            if (item == null) { return false; }
            if(item.ItemTemplate.ItemType != SlotType) { return false; }
            if (SlottedItem != null) { return false; }

            SlottedItem = item;
            return true;
        }
    }
}
namespace OpenRpg.Items.Equipment
{
    public abstract class DefaultEquipmentSlot : IEquipmentSlot
    {
        public abstract bool CanEquipItemType(int itemType);
        
        public IItem SlottedItem { get; private set; }

        protected DefaultEquipmentSlot(IItem slottedItem = null)
        { SlottedItem = slottedItem; }

        public IItem UnequipItem()
        {
            if(SlottedItem == null) { return null; }

            var returningItem = SlottedItem;
            SlottedItem = null;

            return returningItem;
        }

        public bool EquipItemToSlot(IItem item)
        {
            if (item == null) { return false; }
            if(!CanEquipItemType(item.Template.ItemType)) { return false; }
            if (SlottedItem != null) { return false; }

            SlottedItem = item;
            return true;
        }
    }
}
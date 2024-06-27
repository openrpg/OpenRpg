using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Equipment
{
    public abstract class DefaultEquipmentSlot : IEquipmentSlot
    {
        public abstract bool CanEquipItemType(int itemType);
        
        public IItemTemplateInstance SlottedItem { get; private set; }

        protected DefaultEquipmentSlot(IItemTemplateInstance slottedItem = null)
        { SlottedItem = slottedItem; }

        public IItemTemplateInstance UnequipItem()
        {
            if(SlottedItem == null) { return null; }

            var returningItem = SlottedItem;
            SlottedItem = null;

            return returningItem;
        }

        public bool EquipItemToSlot(IItemTemplateInstance item)
        {
            if (item == null) { return false; }
            if(!CanEquipItemType(item.Template.ItemType)) { return false; }
            if (SlottedItem != null) { return false; }

            SlottedItem = item;
            return true;
        }
    }
}
using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items;
using OpenRpg.Items.Equipment;

namespace OpenRpg.Genres.Scifi.Equipment.ShipSlots
{
    public class MiscSlot : DefaultEquipmentSlot
    {
        public MiscSlot(IItem slottedItem = null) : base(slottedItem)
        {}

        public override bool CanEquipItemType(int itemType)
        { return itemType == ShipItemTypes.Misc; }
    }
}
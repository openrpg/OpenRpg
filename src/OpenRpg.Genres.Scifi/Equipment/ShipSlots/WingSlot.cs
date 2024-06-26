using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items;
using OpenRpg.Items.Equipment;

namespace OpenRpg.Genres.Scifi.Equipment.ShipSlots
{
    public class WingSlot : DefaultEquipmentSlot
    {
        public WingSlot(IItem slottedItem = null) : base(slottedItem)
        {}

        public override bool CanEquipItemType(int itemType)
        { return itemType == ShipItemTypes.Wings; }
    }
}
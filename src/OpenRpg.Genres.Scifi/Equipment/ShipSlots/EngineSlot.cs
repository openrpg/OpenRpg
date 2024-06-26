using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items;
using OpenRpg.Items.Equipment;

namespace OpenRpg.Genres.Scifi.Equipment.ShipSlots
{
    public class EngineSlot : DefaultEquipmentSlot
    {
        public EngineSlot(IItem slottedItem = null) : base(slottedItem)
        {}

        public override bool CanEquipItemType(int itemType)
        { return itemType == ShipItemTypes.Engine; }
    }
}
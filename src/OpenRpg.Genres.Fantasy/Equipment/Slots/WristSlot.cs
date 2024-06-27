using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.Equipment;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Fantasy.Equipment.Slots
{
    public class WristSlot : DefaultEquipmentSlot
    {
        public WristSlot(IItemTemplateInstance slottedItem = null) : base(slottedItem)
        {}

        public override bool CanEquipItemType(int itemType)
        { return itemType == FantasyItemTypes.WristItem; }
    }
}
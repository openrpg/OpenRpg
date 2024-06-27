using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items;

using OpenRpg.Items.Equipment;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Fantasy.Equipment.Slots
{
    public class HeadSlot : DefaultEquipmentSlot
    {
        public HeadSlot(IItemTemplateInstance slottedItem = null) : base(slottedItem)
        {}

        public override bool CanEquipItemType(int itemType)
        { return itemType == FantasyItemTypes.HeadItem; }
    }
}
using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items;
using OpenRpg.Items.Equipment;

namespace OpenRpg.Genres.Scifi.Equipment.CharacterSlots
{
    public class ArmourSlot : DefaultEquipmentSlot
    {
        public ArmourSlot(IItem slottedItem = null) : base(slottedItem)
        {}

        public override bool CanEquipItemType(int itemType)
        { return itemType == ScifiItemTypes.Armour; }
    }
}
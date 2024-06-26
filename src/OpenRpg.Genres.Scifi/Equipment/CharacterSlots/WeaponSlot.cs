using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items;
using OpenRpg.Items.Equipment;

namespace OpenRpg.Genres.Scifi.Equipment.CharacterSlots
{
    public class WeaponSlot : DefaultEquipmentSlot
    {
        public WeaponSlot(IItem slottedItem = null) : base(slottedItem)
        {}

        public override bool CanEquipItemType(int itemType)
        { return itemType == ScifiItemTypes.GenericWeapon; }
    }
}
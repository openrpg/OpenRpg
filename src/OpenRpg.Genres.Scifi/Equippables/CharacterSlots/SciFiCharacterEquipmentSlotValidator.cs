using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items.Equippables.Slots;

namespace OpenRpg.Genres.Scifi.Equippables.CharacterSlots
{
    public class SciFiCharacterEquipmentSlotValidator : IEquipmentSlotValidator
    {
        public bool CanEquipItemType(int slotType, int itemType)
        {
            if (slotType == ScifiEntityEquipmentSlotTypes.ArmourSlot) { return itemType == ScifiItemTypes.Armour; }
            if (slotType == ScifiEntityEquipmentSlotTypes.WeaponSlot) { return itemType == ScifiItemTypes.GenericWeapon; }

            return false;
        }
    }
}
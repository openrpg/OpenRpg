using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.Equippables.Slots;

namespace OpenRpg.Genres.Fantasy.Equippables.Validators
{
    public class FantasyCharacterEquipmentSlotValidator : IEquipmentSlotValidator
    {
        public bool CanEquipItemType(int slotType, int itemType)
        {
            if (slotType == FantasyEquipmentSlotTypes.HeadSlot) { return itemType == FantasyItemTypes.HeadItem; }
            if (slotType == FantasyEquipmentSlotTypes.NeckSlot) { return itemType == FantasyItemTypes.NeckItem; }
            if (slotType == FantasyEquipmentSlotTypes.UpperBodySlot) { return itemType == FantasyItemTypes.UpperBodyArmour; }
            if (slotType == FantasyEquipmentSlotTypes.LowerBodySlot) { return itemType == FantasyItemTypes.LowerBodyArmour; }
            if (slotType == FantasyEquipmentSlotTypes.WristSlot) { return itemType == FantasyItemTypes.WristItem; }
            if (slotType == FantasyEquipmentSlotTypes.MainHandSlot) { return itemType == FantasyItemTypes.GenericWeapon; }
            if (slotType == FantasyEquipmentSlotTypes.OffHandSlot) { return itemType == FantasyItemTypes.OffhandItem; }
            if (slotType == FantasyEquipmentSlotTypes.BackSlot) { return itemType == FantasyItemTypes.BackArmour; }
            if (slotType == FantasyEquipmentSlotTypes.Ring1Slot) { return itemType == FantasyItemTypes.RingItem; }
            if (slotType == FantasyEquipmentSlotTypes.Ring2Slot) { return itemType == FantasyItemTypes.RingItem; }
            if (slotType == FantasyEquipmentSlotTypes.FootSlot) { return itemType == FantasyItemTypes.FootArmour; }

            return false;
        }
    }
}
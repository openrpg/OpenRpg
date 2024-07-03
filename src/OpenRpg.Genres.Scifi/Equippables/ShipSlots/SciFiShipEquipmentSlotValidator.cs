using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items.Equippables.Slots;

namespace OpenRpg.Genres.Scifi.Equippables.ShipSlots
{
    public class SciFiShipEquipmentSlotValidator : IEquipmentSlotValidator
    {
        public bool CanEquipItemType(int slotType, int itemType)
        {
            if (slotType == ShipEquipmentSlotTypes.HullArmourSlot) { return itemType == ShipItemTypes.HullArmour; }
            if (slotType == ShipEquipmentSlotTypes.EngineSlot) { return itemType == ShipItemTypes.Engine; }
            if (slotType == ShipEquipmentSlotTypes.ShieldSlot) { return itemType == ShipItemTypes.Shield; }
            if (slotType == ShipEquipmentSlotTypes.WingsSlot) { return itemType == ShipItemTypes.Wings; }
            
            if (slotType == ShipEquipmentSlotTypes.WeaponSlot1) { return itemType == ShipItemTypes.GenericWeapon; }
            if (slotType == ShipEquipmentSlotTypes.WeaponSlot2) { return itemType == ShipItemTypes.GenericWeapon; }
            if (slotType == ShipEquipmentSlotTypes.WeaponSlot3) { return itemType == ShipItemTypes.GenericWeapon; }
            if (slotType == ShipEquipmentSlotTypes.WeaponSlot4) { return itemType == ShipItemTypes.GenericWeapon; }
            if (slotType == ShipEquipmentSlotTypes.WeaponSlot5) { return itemType == ShipItemTypes.GenericWeapon; }
            if (slotType == ShipEquipmentSlotTypes.WeaponSlot6) { return itemType == ShipItemTypes.GenericWeapon; }

            if (slotType == ShipEquipmentSlotTypes.MiscSlot1) { return itemType == ShipItemTypes.Misc; }
            if (slotType == ShipEquipmentSlotTypes.MiscSlot2) { return itemType == ShipItemTypes.Misc; }
            if (slotType == ShipEquipmentSlotTypes.MiscSlot3) { return itemType == ShipItemTypes.Misc; }
            if (slotType == ShipEquipmentSlotTypes.MiscSlot4) { return itemType == ShipItemTypes.Misc; }

            return false;
        }
    }
}
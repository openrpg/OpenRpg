using OpenRpg.Genres.Scifi.Equippables.ShipSlots;
using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class ShipEquipmentExtensions
    {
        public static ItemData WingSlot(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WingsSlot);
        public static ItemData EngineSlot(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.EngineSlot);
        public static ItemData ShieldSlot(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.ShieldSlot);
        public static ItemData HullArmourSlot(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.HullArmourSlot);
        public static ItemData MiscSlot1(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot1);
        public static ItemData MiscSlot2(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot2);
        public static ItemData MiscSlot3(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot3);
        public static ItemData MiscSlot4(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot4);
        public static ItemData WeaponSlot1(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot1);
        public static ItemData WeaponSlot2(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot2);
        public static ItemData WeaponSlot3(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot3);
        public static ItemData WeaponSlot4(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot4);
        public static ItemData WeaponSlot5(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot5);
        public static ItemData WeaponSlot6(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot6);
        
        public static void WingSlot(this ShipEquipment equipment, ItemData itemData) => equipment.Slots[ShipEquipmentSlotTypes.WingsSlot] = itemData;
        public static void EngineSlot(this ShipEquipment equipment, ItemData itemData) => equipment.Slots[ShipEquipmentSlotTypes.EngineSlot] = itemData;
        public static void ShieldSlot(this ShipEquipment equipment, ItemData itemData) => equipment.Slots[ShipEquipmentSlotTypes.ShieldSlot] = itemData;
        public static void HullArmourSlot(this ShipEquipment equipment, ItemData itemData) => equipment.Slots[ShipEquipmentSlotTypes.HullArmourSlot] = itemData;
        public static void MiscSlot1(this ShipEquipment equipment, ItemData itemData) => equipment.Slots[ShipEquipmentSlotTypes.MiscSlot1] = itemData;
        public static void MiscSlot2(this ShipEquipment equipment, ItemData itemData) => equipment.Slots[ShipEquipmentSlotTypes.MiscSlot2] = itemData;
        public static void MiscSlot3(this ShipEquipment equipment, ItemData itemData) => equipment.Slots[ShipEquipmentSlotTypes.MiscSlot3] = itemData;
        public static void MiscSlot4(this ShipEquipment equipment, ItemData itemData) => equipment.Slots[ShipEquipmentSlotTypes.MiscSlot4] = itemData;
        public static void WeaponSlot1(this ShipEquipment equipment, ItemData itemData) => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot1] = itemData;
        public static void WeaponSlot2(this ShipEquipment equipment, ItemData itemData) => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot2] = itemData;
        public static void WeaponSlot3(this ShipEquipment equipment, ItemData itemData) => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot3] = itemData;
        public static void WeaponSlot4(this ShipEquipment equipment, ItemData itemData) => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot4] = itemData;
        public static void WeaponSlot5(this ShipEquipment equipment, ItemData itemData) => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot5] = itemData;
        public static void WeaponSlot6(this ShipEquipment equipment, ItemData itemData) => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot6] = itemData;
    }
}
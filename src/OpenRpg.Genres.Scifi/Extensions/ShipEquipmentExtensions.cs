using OpenRpg.Genres.Scifi.Equippables.ShipSlots;
using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class ShipEquipmentExtensions
    {
        public static Item WingSlot(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WingsSlot);
        public static Item EngineSlot(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.EngineSlot);
        public static Item ShieldSlot(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.ShieldSlot);
        public static Item HullArmourSlot(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.HullArmourSlot);
        public static Item MiscSlot1(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot1);
        public static Item MiscSlot2(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot2);
        public static Item MiscSlot3(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot3);
        public static Item MiscSlot4(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot4);
        public static Item WeaponSlot1(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot1);
        public static Item WeaponSlot2(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot2);
        public static Item WeaponSlot3(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot3);
        public static Item WeaponSlot4(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot4);
        public static Item WeaponSlot5(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot5);
        public static Item WeaponSlot6(this ShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot6);
        
        public static void WingSlot(this ShipEquipment equipment, Item item) => equipment.Slots[ShipEquipmentSlotTypes.WingsSlot] = item;
        public static void EngineSlot(this ShipEquipment equipment, Item item) => equipment.Slots[ShipEquipmentSlotTypes.EngineSlot] = item;
        public static void ShieldSlot(this ShipEquipment equipment, Item item) => equipment.Slots[ShipEquipmentSlotTypes.ShieldSlot] = item;
        public static void HullArmourSlot(this ShipEquipment equipment, Item item) => equipment.Slots[ShipEquipmentSlotTypes.HullArmourSlot] = item;
        public static void MiscSlot1(this ShipEquipment equipment, Item item) => equipment.Slots[ShipEquipmentSlotTypes.MiscSlot1] = item;
        public static void MiscSlot2(this ShipEquipment equipment, Item item) => equipment.Slots[ShipEquipmentSlotTypes.MiscSlot2] = item;
        public static void MiscSlot3(this ShipEquipment equipment, Item item) => equipment.Slots[ShipEquipmentSlotTypes.MiscSlot3] = item;
        public static void MiscSlot4(this ShipEquipment equipment, Item item) => equipment.Slots[ShipEquipmentSlotTypes.MiscSlot4] = item;
        public static void WeaponSlot1(this ShipEquipment equipment, Item item) => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot1] = item;
        public static void WeaponSlot2(this ShipEquipment equipment, Item item) => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot2] = item;
        public static void WeaponSlot3(this ShipEquipment equipment, Item item) => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot3] = item;
        public static void WeaponSlot4(this ShipEquipment equipment, Item item) => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot4] = item;
        public static void WeaponSlot5(this ShipEquipment equipment, Item item) => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot5] = item;
        public static void WeaponSlot6(this ShipEquipment equipment, Item item) => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot6] = item;
    }
}
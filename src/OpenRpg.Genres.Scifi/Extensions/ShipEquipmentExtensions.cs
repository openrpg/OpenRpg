using OpenRpg.Genres.Scifi.Equipment;
using OpenRpg.Genres.Scifi.Equipment.ShipSlots;
using OpenRpg.Genres.Scifi.Types;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class ShipEquipmentExtensions
    {
        public static WingSlot WingSlot(this IShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WingsSlot) as WingSlot;
        public static EngineSlot EngineSlot(this IShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.EngineSlot) as EngineSlot;
        public static ShieldSlot ShieldSlot(this IShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.ShieldSlot) as ShieldSlot;
        public static HullArmourSlot HullArmourSlot(this IShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.HullArmourSlot) as HullArmourSlot;
        public static MiscSlot MiscSlot1(this IShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot1) as MiscSlot;
        public static MiscSlot MiscSlot2(this IShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot2) as MiscSlot;
        public static MiscSlot MiscSlot3(this IShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot3) as MiscSlot;
        public static MiscSlot MiscSlot4(this IShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot4) as MiscSlot;
        public static WeaponSlot WeaponSlot1(this IShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot1) as WeaponSlot;
        public static WeaponSlot WeaponSlot2(this IShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot2) as WeaponSlot;
        public static WeaponSlot WeaponSlot3(this IShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot3) as WeaponSlot;
        public static WeaponSlot WeaponSlot4(this IShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot4) as WeaponSlot;
        public static WeaponSlot WeaponSlot5(this IShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot5) as WeaponSlot;
        public static WeaponSlot WeaponSlot6(this IShipEquipment equipment) => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot6) as WeaponSlot;
    }
}
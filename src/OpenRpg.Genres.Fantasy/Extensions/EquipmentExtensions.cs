using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Equippables.Slots;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class EquipmentExtensions
    {
        public static Item BackSlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.BackSlot);
        public static Item FootSlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.FootSlot);
        public static Item HeadSlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.HeadSlot);
        public static Item NeckSlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.NeckSlot);
        public static Item Ring1Slot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.Ring1Slot);
        public static Item Ring2Slot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.Ring2Slot);
        public static Item WristSlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.WristSlot);
        public static Item OffHandSlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.OffHandSlot);
        public static Item MainHandSlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.MainHandSlot);
        public static Item LowerBodySlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.LowerBodySlot);
        public static Item UpperBodySlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.UpperBodySlot);

        public static void BackSlot(this Equipment equipment, Item item) => equipment.Slots[FantasyEquipmentSlotTypes.BackSlot] = item;
        public static void FootSlot(this Equipment equipment, Item item) => equipment.Slots[FantasyEquipmentSlotTypes.FootSlot] = item;
        public static void HeadSlot(this Equipment equipment, Item item) => equipment.Slots[FantasyEquipmentSlotTypes.HeadSlot] = item;
        public static void NeckSlot(this Equipment equipment, Item item) => equipment.Slots[FantasyEquipmentSlotTypes.NeckSlot] = item;
        public static void Ring1Slot(this Equipment equipment, Item item) => equipment.Slots[FantasyEquipmentSlotTypes.Ring1Slot] = item;
        public static void Ring2Slot(this Equipment equipment, Item item) => equipment.Slots[FantasyEquipmentSlotTypes.Ring2Slot] = item;
        public static void WristSlot(this Equipment equipment, Item item) => equipment.Slots[FantasyEquipmentSlotTypes.WristSlot] = item;
        public static void OffHandSlot(this Equipment equipment, Item item) => equipment.Slots[FantasyEquipmentSlotTypes.OffHandSlot] = item;
        public static void MainHandSlot(this Equipment equipment, Item item) => equipment.Slots[FantasyEquipmentSlotTypes.MainHandSlot] = item;
        public static void LowerBodySlot(this Equipment equipment, Item item) => equipment.Slots[FantasyEquipmentSlotTypes.LowerBodySlot] = item;
        public static void UpperBodySlot(this Equipment equipment, Item item) => equipment.Slots[FantasyEquipmentSlotTypes.UpperBodySlot] = item;
    }
}
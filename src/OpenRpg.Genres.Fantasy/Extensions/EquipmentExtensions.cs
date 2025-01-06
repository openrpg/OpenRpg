using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Equippables.Slots;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class EquipmentExtensions
    {
        public static ItemData BackSlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.BackSlot);
        public static ItemData FootSlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.FootSlot);
        public static ItemData HeadSlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.HeadSlot);
        public static ItemData NeckSlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.NeckSlot);
        public static ItemData Ring1Slot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.Ring1Slot);
        public static ItemData Ring2Slot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.Ring2Slot);
        public static ItemData WristSlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.WristSlot);
        public static ItemData OffHandSlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.OffHandSlot);
        public static ItemData MainHandSlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.MainHandSlot);
        public static ItemData LowerBodySlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.LowerBodySlot);
        public static ItemData UpperBodySlot(this Equipment equipment) => equipment.Slots.Get(FantasyEquipmentSlotTypes.UpperBodySlot);

        public static void BackSlot(this Equipment equipment, ItemData itemData) => equipment.Slots[FantasyEquipmentSlotTypes.BackSlot] = itemData;
        public static void FootSlot(this Equipment equipment, ItemData itemData) => equipment.Slots[FantasyEquipmentSlotTypes.FootSlot] = itemData;
        public static void HeadSlot(this Equipment equipment, ItemData itemData) => equipment.Slots[FantasyEquipmentSlotTypes.HeadSlot] = itemData;
        public static void NeckSlot(this Equipment equipment, ItemData itemData) => equipment.Slots[FantasyEquipmentSlotTypes.NeckSlot] = itemData;
        public static void Ring1Slot(this Equipment equipment, ItemData itemData) => equipment.Slots[FantasyEquipmentSlotTypes.Ring1Slot] = itemData;
        public static void Ring2Slot(this Equipment equipment, ItemData itemData) => equipment.Slots[FantasyEquipmentSlotTypes.Ring2Slot] = itemData;
        public static void WristSlot(this Equipment equipment, ItemData itemData) => equipment.Slots[FantasyEquipmentSlotTypes.WristSlot] = itemData;
        public static void OffHandSlot(this Equipment equipment, ItemData itemData) => equipment.Slots[FantasyEquipmentSlotTypes.OffHandSlot] = itemData;
        public static void MainHandSlot(this Equipment equipment, ItemData itemData) => equipment.Slots[FantasyEquipmentSlotTypes.MainHandSlot] = itemData;
        public static void LowerBodySlot(this Equipment equipment, ItemData itemData) => equipment.Slots[FantasyEquipmentSlotTypes.LowerBodySlot] = itemData;
        public static void UpperBodySlot(this Equipment equipment, ItemData itemData) => equipment.Slots[FantasyEquipmentSlotTypes.UpperBodySlot] = itemData;
    }
}
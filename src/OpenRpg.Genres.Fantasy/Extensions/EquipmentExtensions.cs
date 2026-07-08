using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Fantasy.Extensions
{
    public static class EquipmentExtensions
    {
        public static void PopulateFantasySlots(this Equipment equipment)
        {
            var slots = equipment.Slots;
            slots[FantasyEquipmentSlotTypes.HeadSlot] =  null;
            slots[FantasyEquipmentSlotTypes.BackSlot] =  null;
            slots[FantasyEquipmentSlotTypes.UpperBodySlot] =  null;
            slots[FantasyEquipmentSlotTypes.LowerBodySlot] =  null;
            slots[FantasyEquipmentSlotTypes.FootSlot] =  null;
            slots[FantasyEquipmentSlotTypes.MainHandSlot] =  null;
            slots[FantasyEquipmentSlotTypes.OffHandSlot] =  null;
            slots[FantasyEquipmentSlotTypes.NeckSlot] =  null;
            slots[FantasyEquipmentSlotTypes.Ring1Slot] =  null;
            slots[FantasyEquipmentSlotTypes.Ring2Slot] =  null;
            slots[FantasyEquipmentSlotTypes.WristSlot] =  null;
        }
        
        extension(Equipment equipment)
        {
            public ItemData BackSlot
            {
                get => equipment.Slots.Get(FantasyEquipmentSlotTypes.BackSlot);
                set => equipment.Slots[FantasyEquipmentSlotTypes.BackSlot] = value;
            }

            public ItemData FootSlot
            {
                get => equipment.Slots.Get(FantasyEquipmentSlotTypes.FootSlot);
                set => equipment.Slots[FantasyEquipmentSlotTypes.FootSlot] = value;
            }

            public ItemData HeadSlot
            {
                get => equipment.Slots.Get(FantasyEquipmentSlotTypes.HeadSlot);
                set => equipment.Slots[FantasyEquipmentSlotTypes.HeadSlot] = value;
            }

            public ItemData NeckSlot
            {
                get => equipment.Slots.Get(FantasyEquipmentSlotTypes.NeckSlot);
                set => equipment.Slots[FantasyEquipmentSlotTypes.NeckSlot] = value;
            }

            public ItemData Ring1Slot
            {
                get => equipment.Slots.Get(FantasyEquipmentSlotTypes.Ring1Slot);
                set => equipment.Slots[FantasyEquipmentSlotTypes.Ring1Slot] = value;
            }

            public ItemData Ring2Slot
            {
                get => equipment.Slots.Get(FantasyEquipmentSlotTypes.Ring2Slot);
                set => equipment.Slots[FantasyEquipmentSlotTypes.Ring2Slot] = value;
            }

            public ItemData WristSlot
            {
                get => equipment.Slots.Get(FantasyEquipmentSlotTypes.WristSlot);
                set => equipment.Slots[FantasyEquipmentSlotTypes.WristSlot] = value;
            }

            public ItemData OffHandSlot
            {
                get => equipment.Slots.Get(FantasyEquipmentSlotTypes.OffHandSlot);
                set => equipment.Slots[FantasyEquipmentSlotTypes.OffHandSlot] = value;
            }

            public ItemData MainHandSlot
            {
                get => equipment.Slots.Get(FantasyEquipmentSlotTypes.MainHandSlot);
                set => equipment.Slots[FantasyEquipmentSlotTypes.MainHandSlot] = value;
            }

            public ItemData LowerBodySlot
            {
                get => equipment.Slots.Get(FantasyEquipmentSlotTypes.LowerBodySlot);
                set => equipment.Slots[FantasyEquipmentSlotTypes.LowerBodySlot] = value;
            }

            public ItemData UpperBodySlot
            {
                get => equipment.Slots.Get(FantasyEquipmentSlotTypes.UpperBodySlot);
                set => equipment.Slots[FantasyEquipmentSlotTypes.UpperBodySlot] = value;
            }
        }
    }
}
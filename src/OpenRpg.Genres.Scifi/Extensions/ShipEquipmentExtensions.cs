using OpenRpg.Genres.Scifi.Equippables.ShipSlots;
using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class ShipEquipmentExtensions
    {
        extension(ShipEquipment equipment)
        {
            public ItemData WingsSlot
            {
                get => equipment.Slots.Get(ShipEquipmentSlotTypes.WingsSlot);
                set => equipment.Slots[ShipEquipmentSlotTypes.WingsSlot] = value;
            }
            
            public ItemData EngineSlot
            {
                get => equipment.Slots.Get(ShipEquipmentSlotTypes.EngineSlot);
                set => equipment.Slots[ShipEquipmentSlotTypes.EngineSlot] = value;
            }
            
            public ItemData ShieldSlot
            {
                get => equipment.Slots.Get(ShipEquipmentSlotTypes.ShieldSlot);
                set => equipment.Slots[ShipEquipmentSlotTypes.ShieldSlot] = value;
            }
            
            public ItemData HullArmourSlot
            {
                get => equipment.Slots.Get(ShipEquipmentSlotTypes.HullArmourSlot);
                set => equipment.Slots[ShipEquipmentSlotTypes.HullArmourSlot] = value;
            }
            
            public ItemData MiscSlot1
            {
                get => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot1);
                set => equipment.Slots[ShipEquipmentSlotTypes.MiscSlot1] = value;
            }
            
            public ItemData MiscSlot2
            {
                get => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot2);
                set => equipment.Slots[ShipEquipmentSlotTypes.MiscSlot2] = value;
            }
            
            public ItemData MiscSlot3
            {
                get => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot3);
                set => equipment.Slots[ShipEquipmentSlotTypes.MiscSlot3] = value;
            }
            
            public ItemData MiscSlot4
            {
                get => equipment.Slots.Get(ShipEquipmentSlotTypes.MiscSlot4);
                set => equipment.Slots[ShipEquipmentSlotTypes.MiscSlot4] = value;
            }
            
            public ItemData WeaponSlot1
            {
                get => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot1);
                set => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot1] = value;
            }
            
            public ItemData WeaponSlot2
            {
                get => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot2);
                set => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot2] = value;
            }
            
            public ItemData WeaponSlot3
            {
                get => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot3);
                set => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot3] = value;
            }
            
            public ItemData WeaponSlot4
            {
                get => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot4);
                set => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot4] = value;
            }
            
            public ItemData WeaponSlot5
            {
                get => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot5);
                set => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot5] = value;
            }
            
            public ItemData WeaponSlot6
            {
                get => equipment.Slots.Get(ShipEquipmentSlotTypes.WeaponSlot6);
                set => equipment.Slots[ShipEquipmentSlotTypes.WeaponSlot6] = value;
            }
        }
    }
}
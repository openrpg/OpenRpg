using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class EquipmentExtensions
    {
        extension(Equipment equipment)
        {
            public ItemData WeaponSlot
            {
                get => equipment.Slots.Get(ScifiEntityEquipmentSlotTypes.WeaponSlot);
                set => equipment.Slots[ScifiEntityEquipmentSlotTypes.WeaponSlot] = value;
            }
            
            public ItemData ArmourSlot
            {
                get => equipment.Slots.Get(ScifiEntityEquipmentSlotTypes.ArmourSlot);
                set => equipment.Slots[ScifiEntityEquipmentSlotTypes.ArmourSlot] = value;
            }
        }
    }
}
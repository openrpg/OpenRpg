using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class EquipmentExtensions
    {
        public static ItemData WeaponSlot(this Equipment equipment) => equipment.Slots.Get(ScifiEntityEquipmentSlotTypes.WeaponSlot);
        public static ItemData ArmourSlot(this Equipment equipment) => equipment.Slots.Get(ScifiEntityEquipmentSlotTypes.ArmourSlot);
        
        public static void WeaponSlot(this Equipment equipment, ItemData itemData) => equipment.Slots[ScifiEntityEquipmentSlotTypes.WeaponSlot] = itemData;
        public static void ArmourSlot(this Equipment equipment, ItemData itemData) => equipment.Slots[ScifiEntityEquipmentSlotTypes.ArmourSlot] = itemData;
    }
}
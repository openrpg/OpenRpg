using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Templates;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class EquipmentExtensions
    {
        public static Item WeaponSlot(this Equipment equipment) => equipment.Slots.Get(ScifiEntityEquipmentSlotTypes.WeaponSlot);
        public static Item ArmourSlot(this Equipment equipment) => equipment.Slots.Get(ScifiEntityEquipmentSlotTypes.ArmourSlot);
        
        public static void WeaponSlot(this Equipment equipment, Item item) => equipment.Slots[ScifiEntityEquipmentSlotTypes.WeaponSlot] = item;
        public static void ArmourSlot(this Equipment equipment, Item item) => equipment.Slots[ScifiEntityEquipmentSlotTypes.ArmourSlot] = item;
    }
}
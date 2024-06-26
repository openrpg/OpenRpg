using OpenRpg.Genres.Scifi.Equipment;
using OpenRpg.Genres.Scifi.Equipment.CharacterSlots;
using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Items.Equipment;

namespace OpenRpg.Genres.Scifi.Extensions
{
    public static class EquipmentExtensions
    {
        public static WeaponSlot WeaponSlot(this IEquipment equipment) => equipment.Slots.Get(ScifiEntityEquipmentSlotTypes.WeaponSlot) as WeaponSlot;
        public static ArmourSlot ArmourSlot(this IEquipment equipment) => equipment.Slots.Get(ScifiEntityEquipmentSlotTypes.ArmourSlot) as ArmourSlot;
    }
}
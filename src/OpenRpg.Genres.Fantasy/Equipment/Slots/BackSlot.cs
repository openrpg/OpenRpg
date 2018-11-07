using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items;
using OpenRpg.Items.Defaults;

namespace OpenRpg.Genres.Fantasy.Equipment.Slots
{
    public class BackSlot : DefaultEquipmentSlot<IItem>
    {
        public override int SlotType => ItemTypes.BackArmour;
    }
}
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items;
using OpenRpg.Items.Defaults;

namespace OpenRpg.Genres.Fantasy.Equipment.Slots
{
    public class FootSlot : DefaultEquipmentSlot<IItem>
    {
        public override int SlotType => ItemTypes.FootArmour;
    }
}
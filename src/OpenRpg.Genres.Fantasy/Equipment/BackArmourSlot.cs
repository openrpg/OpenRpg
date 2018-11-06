using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items;
using OpenRpg.Items.Defaults;

namespace OpenRpg.Genres.Fantasy.Equipment
{
    public class BackArmourSlot : DefaultEquipmentSlot<IItem>
    {
        public override int SlotType => ItemTypes.BackArmour;
    }
}
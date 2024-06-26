using OpenRpg.Genres.Persistence.Characters;
using OpenRpg.Genres.Persistence.Classes;
using OpenRpg.Genres.Persistence.Items;
using OpenRpg.Genres.Persistence.Items.Inventory;
using OpenRpg.Genres.Scifi.Persistence.Items.Equipment;

namespace OpenRpg.Genres.Scifi.Persistence.Character
{
    public abstract class SciFiCharacterMapper : CharacterMapper, ISciFiCharacterMapper
    {
        protected SciFiCharacterMapper(ItemMapper itemMapper, IClassMapper classMapper, IMultiClassMapper multiClassMapper, ISciFiCharacterEquipmentMapper equipmentMapper, InventoryMapper inventoryMapper) 
            : base(itemMapper, classMapper, multiClassMapper, equipmentMapper, inventoryMapper)
        {
        }
    }
}
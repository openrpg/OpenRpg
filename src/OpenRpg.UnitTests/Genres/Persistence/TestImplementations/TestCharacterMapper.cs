using OpenRpg.Core.Races;
using OpenRpg.Genres.Persistence.Characters;
using OpenRpg.Genres.Persistence.Classes;
using OpenRpg.Genres.Persistence.Items;
using OpenRpg.Genres.Persistence.Items.Equipment;
using OpenRpg.Genres.Persistence.Items.Inventory;

namespace OpenRpg.UnitTests.Genres.Persistence.TestImplementations
{
    public class TestCharacterMapper : CharacterMapper
    {
        private readonly DefaultRaceTemplate DefaultRaceTemplate = new DefaultRaceTemplate() { Id = 1 };
        
        public TestCharacterMapper(IItemMapper itemMapper, IClassMapper classMapper, IMultiClassMapper multiClassMapper, IEquipmentMapper equipmentMapper, IInventoryMapper inventoryMapper) 
            : base(itemMapper, classMapper, multiClassMapper, equipmentMapper, inventoryMapper)
        {
        }

        public override IRaceTemplate GetRaceTemplateFor(int raceTemplateId)
        {
            return DefaultRaceTemplate;
        }
    }
}
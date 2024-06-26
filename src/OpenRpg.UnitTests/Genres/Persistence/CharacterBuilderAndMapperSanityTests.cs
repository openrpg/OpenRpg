using Moq;
using Newtonsoft.Json;
using OpenRpg.Core.Utils;
using OpenRpg.Genres.Builders;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Persistence.Items.Equipment;
using OpenRpg.Genres.Persistence.Classes;
using OpenRpg.Genres.Persistence.Items.Inventory;
using OpenRpg.UnitTests.Genres.Persistence.TestImplementations;
using Xunit;

namespace OpenRpg.UnitTests.Genres.Persistence
{
    public class CharacterBuilderAndMapperSanityTests
    {
        [Fact]
        public void should_build_and_map_correctly()
        {
            var mockRandomizer = new Mock<IRandomizer>();
            
            var itemMapper = new TestItemMapper();
            var classMapper = new TestClassMapper();
            var characterMapper = new TestCharacterMapper(new TestItemMapper(), classMapper, 
                new MultiClassMapper(classMapper), new FantasyEquipmentMapper(itemMapper), 
                new InventoryMapper(itemMapper));
            
            var characterBuilder = new CharacterBuilder(characterMapper, mockRandomizer.Object);
            var character = characterBuilder
                .CreateNew()
                .Build();
            
            Assert.NotNull(character);

            var characterData = character.ToDataModel();
            var characterRegenerated = characterMapper.Map(characterData);
            
            Assert.NotNull(characterRegenerated);

            var expected = JsonConvert.SerializeObject(character);
            var actual = JsonConvert.SerializeObject(characterRegenerated);
            Assert.Equal(expected, actual);
        }
    }
}
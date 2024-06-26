using Moq;
using Newtonsoft.Json;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Utils;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Fantasy.Builders;
using OpenRpg.Genres.Fantasy.Persistence.Items.Equipment;
using OpenRpg.Genres.Persistence.Characters;
using OpenRpg.Genres.Persistence.Classes;
using OpenRpg.Genres.Persistence.Items.Inventory;
using OpenRpg.Items;
using OpenRpg.Items.Templates;
using OpenRpg.UnitTests.Genres.Persistence.TestImplementations;
using Xunit;

namespace OpenRpg.UnitTests.Genres.Fantasy
{
    public class FantasyCharacterBuilderAndMapperSanityTests
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
            
            var characterBuilder = new FantasyCharacterBuilder(characterMapper, mockRandomizer.Object);
            var character = characterBuilder
                .CreateNew()
                .WithGender(1)
                .WithRaceId(1)
                .WithClassId(1, 1)
                .WithInventoryItem(new DefaultItem(){ Template = new DefaultItemTemplate() })
                .Build();
            
            Assert.NotNull(character);

            var characterData = character.ToDataModel();
            var characterRegenerated = characterMapper.Map(characterData);
            
            Assert.NotNull(characterRegenerated);

            // We just serialize the 2 objects to make deep comparison easier
            var expected = JsonConvert.SerializeObject(character);
            var actual = JsonConvert.SerializeObject(characterRegenerated);
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void should_json_serialization_sanity_test()
        {
            var mockRandomizer = new Mock<IRandomizer>();
            
            var itemMapper = new TestItemMapper();
            var classMapper = new TestClassMapper();
            var characterMapper = new TestCharacterMapper(new TestItemMapper(), classMapper,
                new MultiClassMapper(classMapper), new FantasyEquipmentMapper(itemMapper), 
                new InventoryMapper(itemMapper));
            
            var characterBuilder = new FantasyCharacterBuilder(characterMapper, mockRandomizer.Object);
            var character = characterBuilder
                .CreateNew()
                .WithGender(1)
                .WithRaceId(1)
                .WithClassId(1, 1)
                .WithInventoryItem(new DefaultItem(){ Template = new DefaultItemTemplate() })
                .Build();
            
            Assert.NotNull(character);

            var characterData = character.ToDataModel();

            var jsonSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            };
            
            var jsonData = JsonConvert.SerializeObject(characterData, jsonSettings);
            var recreatedCharacterData = JsonConvert.DeserializeObject<CharacterData>(jsonData, jsonSettings);
            var characterRegenerated = characterMapper.Map(recreatedCharacterData);

            var level = characterRegenerated.Variables.Class().Variables.Level();
            var gender = characterRegenerated.Variables.Gender();
            
            var expected = JsonConvert.SerializeObject(character);
            var actual = JsonConvert.SerializeObject(characterRegenerated);
            Assert.Equal(expected, actual);
        }
    }
}
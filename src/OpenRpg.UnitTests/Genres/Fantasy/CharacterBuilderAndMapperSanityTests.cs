using System;
using Moq;
using Newtonsoft.Json;
using OpenRpg.Core.Utils;
using OpenRpg.Genres.Characters;
using OpenRpg.Genres.Fantasy.Builders;
using OpenRpg.Items.Templates;
using Xunit;
using Xunit.Abstractions;

namespace OpenRpg.UnitTests.Genres.Fantasy
{
    public class FantasyCharacterBuilderAndMapperSanityTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public FantasyCharacterBuilderAndMapperSanityTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void should_json_serialization_sanity_test()
        {
            var mockRandomizer = new Mock<IRandomizer>();
            
            var characterBuilder = new FantasyCharacterBuilder(mockRandomizer.Object);
            var character = characterBuilder
                .CreateNew()
                .WithGender(1)
                .WithRaceId(1)
                .WithClassId(1, 1)
                .WithInventoryItem(new ItemData(){ TemplateId = 1 })
                .Build();
            
            Assert.NotNull(character);

            var jsonSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            
            var jsonData = JsonConvert.SerializeObject(character, jsonSettings);
            var recreatedCharacterData = JsonConvert.DeserializeObject<Character>(jsonData, jsonSettings);
            var recreatedJson = JsonConvert.SerializeObject(recreatedCharacterData, jsonSettings);
            
            _testOutputHelper.WriteLine(jsonData);
            Assert.Equal(jsonData, recreatedJson);
        }
    }
}
using System;
using System.Linq;
using Newtonsoft.Json;
using OpenRpg.Genres.Extensions;
using OpenRpg.Genres.Persistence.Items.Inventory;
using OpenRpg.Items;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventory;
using OpenRpg.Items.Templates;
using OpenRpg.UnitTests.Genres.Persistence.TestImplementations;
using Xunit;

namespace OpenRpg.UnitTests.Genres.Persistence
{
    public class InventoryBuilderAndMapperSanityTests
    {
        [Fact]
        public void should_build_and_map_correctly()
        {
            var dummyItemTemplate1 = new DefaultItemTemplate() { Id = 1 };
            var dummyItem1 = new DefaultItem() { UniqueId = Guid.NewGuid(), Template = dummyItemTemplate1 };
            dummyItem1.Variables.Amount(10);
            
            var dummyItemTemplate2 = new DefaultItemTemplate() { Id = 2 };
            var dummyItem2 = new DefaultItem() { UniqueId = Guid.NewGuid(), Template = dummyItemTemplate2 };
            dummyItem2.Variables.Amount(25);
            
            var inventory = new DefaultInventory();
            inventory.AddItem(dummyItem1);
            inventory.AddItem(dummyItem2);
            
            var inventoryMapper = new InventoryMapper(new TestItemMapper());
            var inventoryData = inventory.ToDataModel();
            var regeneratedInventory = inventoryMapper.Map(inventoryData);
            Assert.NotNull(regeneratedInventory);
            
            // Manually re-add template types
            ((DefaultItem)regeneratedInventory.Items.Single(x => x.Variables.Amount() == 10)).Template = dummyItemTemplate1;
            ((DefaultItem)regeneratedInventory.Items.Single(x => x.Variables.Amount() == 25)).Template = dummyItemTemplate2;
            
            var expected = JsonConvert.SerializeObject(inventory);
            var actual = JsonConvert.SerializeObject(regeneratedInventory);
            Assert.Equal(expected, actual);

            var deserialized = JsonConvert.DeserializeObject<InventoryData>(actual);
            Assert.Equal(inventoryData.Items.Count, deserialized.Items.Count);
        }
    }
}
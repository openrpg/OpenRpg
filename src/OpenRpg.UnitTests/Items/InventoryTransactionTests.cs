using System.Collections.Generic;
using System.Linq;
using Moq;
using OpenRpg.Core.Templates;
using OpenRpg.Items;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventories;
using OpenRpg.Items.Templates;
using Xunit;

namespace OpenRpg.UnitTests.Items;

public class InventoryTransactionTests
{
    [Fact]
    public void should_carry_out_transaction_successfully()
    {
        var dummyItemTemplate1 = new ItemTemplate() { Id = 123 };
        var dummyItemTemplate2 = new ItemTemplate() { Id = 345 };
        var dummyItemTemplate3 = new ItemTemplate() { Id = 567 };
        var dummyItemTemplate4 = new ItemTemplate() { Id = 789 };

        var itemToRemove1 = new ItemData() { TemplateId = dummyItemTemplate1.Id };
        itemToRemove1.Variables.Amount(5);

        var itemToRemove2 = new ItemData() { TemplateId = dummyItemTemplate2.Id };
        itemToRemove2.Variables.Amount(2);
        
        var itemToAdd1 = new ItemData() { TemplateId = dummyItemTemplate3.Id };
        itemToAdd1.Variables.Amount(1);

        var itemToAdd2 = new ItemData() { TemplateId = dummyItemTemplate4.Id };
        itemToAdd2.Variables.Amount(10);

        var inventory = new Inventory()
        {
            Items = new List<ItemData>() { itemToRemove1.Clone(), itemToRemove2.Clone() }
        };

        var mockTemplateAccessor = new Mock<ITemplateAccessor>();
        mockTemplateAccessor
            .Setup(x => x.Get<ItemTemplate>(It.Is<int>(y => y == dummyItemTemplate1.Id)))
            .Returns(dummyItemTemplate1);
        
        mockTemplateAccessor
            .Setup(x => x.Get<ItemTemplate>(It.Is<int>(y => y == dummyItemTemplate2.Id)))
            .Returns(dummyItemTemplate2);
        
        mockTemplateAccessor
            .Setup(x => x.Get<ItemTemplate>(It.Is<int>(y => y == dummyItemTemplate3.Id)))
            .Returns(dummyItemTemplate3);
        
        mockTemplateAccessor
            .Setup(x => x.Get<ItemTemplate>(It.Is<int>(y => y == dummyItemTemplate4.Id)))
            .Returns(dummyItemTemplate4);
        
        var succeeeded = inventory.CreateTransaction(mockTemplateAccessor.Object)
            .RemoveItems(new[] { itemToRemove1, itemToRemove2 })
            .AddItems(new[] { itemToAdd1, itemToAdd2 })
            .ApplyChanges();

        Assert.True(succeeeded);
        Assert.Equal(2, inventory.Items.Count);
        Assert.False(inventory.HasItem(dummyItemTemplate1.Id));
        Assert.False(inventory.HasItem(dummyItemTemplate2.Id));

        Assert.True(inventory.HasItem((dummyItemTemplate3.Id)));
        var item1 = inventory.Items.Single(x => x.TemplateId == itemToAdd1.TemplateId);
        Assert.Equal(itemToAdd1.Variables.Amount(), item1.Variables.Amount());

        Assert.True(inventory.HasItem((dummyItemTemplate4.Id)));
        var item2 = inventory.Items.Single(x => x.TemplateId == itemToAdd2.TemplateId);
        Assert.Equal(itemToAdd2.Variables.Amount(), item2.Variables.Amount());
    }
}
using System.Collections.Generic;
using System.Linq;
using Moq;
using OpenRpg.Core.Templates;
using OpenRpg.Items;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventories;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;
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
        itemToRemove1.Variables.Amount = 5;

        var itemToRemove2 = new ItemData() { TemplateId = dummyItemTemplate2.Id };
        itemToRemove2.Variables.Amount = 2;

        var itemToAdd1 = new ItemData() { TemplateId = dummyItemTemplate3.Id };
        itemToAdd1.Variables.Amount = 1;

        var itemToAdd2 = new ItemData() { TemplateId = dummyItemTemplate4.Id };
        itemToAdd2.Variables.Amount = 10;

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
        Assert.Equal(itemToAdd1.Variables.Amount, item1.Variables.Amount);

        Assert.True(inventory.HasItem((dummyItemTemplate4.Id)));
        var item2 = inventory.Items.Single(x => x.TemplateId == itemToAdd2.TemplateId);
        Assert.Equal(itemToAdd2.Variables.Amount, item2.Variables.Amount);
    }

    [Fact]
    public void should_fail_fast_when_trying_to_remove_item_not_in_inventory()
    {
        var dummyItemTemplate = new ItemTemplate() { Id = 100 };
        var missingItemTemplate = new ItemTemplate() { Id = 200 };

        var itemInInventory = new ItemData { TemplateId = dummyItemTemplate.Id };
        itemInInventory.Variables.Amount = 5;

        var inventory = new Inventory { Items = new List<ItemData> { itemInInventory.Clone() } };

        var removalOfMissing = new ItemData { TemplateId = missingItemTemplate.Id };
        removalOfMissing.Variables.Amount = 1;

        var mockTemplateAccessor = new Mock<ITemplateAccessor>();
        mockTemplateAccessor
            .Setup(x => x.Get<ItemTemplate>(It.IsAny<int>()))
            .Returns((int id) => id == dummyItemTemplate.Id ? dummyItemTemplate : missingItemTemplate);

        var succeeded = inventory.CreateTransaction(mockTemplateAccessor.Object)
            .RemoveItems(new[] { removalOfMissing })
            .ApplyChanges();

        Assert.False(succeeded);
        Assert.Single(inventory.Items);
        Assert.Equal(5, inventory.Items[0].Variables.Amount);
    }

    [Fact]
    public void should_rollback_all_additions_when_a_later_addition_fails_for_non_amounted_items()
    {
        var dummyItemTemplate1 = new ItemTemplate { Id = 100 };
        var dummyItemTemplate2 = new ItemTemplate { Id = 200 };
        var dummyItemTemplate3 = new ItemTemplate { Id = 300 };

        var inventory = new Inventory
        {
            Items = new List<ItemData>(),
            Variables = new InventoryVariables()
        };
        inventory.Variables[OpenRpg.Items.Types.InventoryVariableTypes.MaxSlots] = 2;

        var firstNonAmountedItem = new ItemData { TemplateId = dummyItemTemplate1.Id };
        var secondNonAmountedItem = new ItemData { TemplateId = dummyItemTemplate2.Id };
        var thirdNonAmountedItem = new ItemData { TemplateId = dummyItemTemplate3.Id };

        var mockTemplateAccessor = new Mock<ITemplateAccessor>();
        mockTemplateAccessor
            .Setup(x => x.Get<ItemTemplate>(It.IsAny<int>()))
            .Returns((int id) => id switch
            {
                100 => dummyItemTemplate1,
                200 => dummyItemTemplate2,
                _ => dummyItemTemplate3
            });

        var succeeded = inventory.CreateTransaction(mockTemplateAccessor.Object)
            .AddItems(new[] { firstNonAmountedItem, secondNonAmountedItem, thirdNonAmountedItem })
            .ApplyChanges();

        Assert.False(succeeded);
        Assert.Empty(inventory.Items);
    }

    [Fact]
    public void should_rollback_all_additions_when_a_later_addition_fails_for_amounted_items()
    {
        var dummyItemTemplate1 = new ItemTemplate { Id = 100 };
        var dummyItemTemplate2 = new ItemTemplate { Id = 200 };

        var inventory = new Inventory
        {
            Items = new List<ItemData>(),
            Variables = new InventoryVariables()
        };
        inventory.Variables[OpenRpg.Items.Types.InventoryVariableTypes.MaxSlots] = 1;

        var firstStackedItem = new ItemData { TemplateId = dummyItemTemplate1.Id };
        firstStackedItem.Variables.Amount = 1;

        var secondStackedItem = new ItemData { TemplateId = dummyItemTemplate2.Id };
        secondStackedItem.Variables.Amount = 5;

        var mockTemplateAccessor = new Mock<ITemplateAccessor>();
        mockTemplateAccessor
            .Setup(x => x.Get<ItemTemplate>(It.IsAny<int>()))
            .Returns((int id) => id == 100 ? dummyItemTemplate1 : dummyItemTemplate2);

        var succeeded = inventory.CreateTransaction(mockTemplateAccessor.Object)
            .AddItems(new[] { firstStackedItem, secondStackedItem })
            .ApplyChanges();

        Assert.False(succeeded);
        Assert.Empty(inventory.Items);
    }

    [Fact]
    public void should_rollback_removals_when_an_addition_fails()
    {
        var dummyItemTemplate1 = new ItemTemplate { Id = 100 };
        dummyItemTemplate1.Variables.MaxStacks = 1;

        var dummyItemTemplate2 = new ItemTemplate { Id = 200 };
        dummyItemTemplate2.Variables.MaxStacks = 1;

        var itemInInventory = new ItemData { TemplateId = dummyItemTemplate1.Id };
        itemInInventory.Variables.Amount = 1;

        var inventory = new Inventory
        {
            Items = new List<ItemData> { itemInInventory.Clone() },
            Variables = new InventoryVariables()
        };
        inventory.Variables[OpenRpg.Items.Types.InventoryVariableTypes.MaxSlots] = 1;

        var itemToRemove = new ItemData { TemplateId = dummyItemTemplate1.Id };
        itemToRemove.Variables.Amount = 1;

        var firstAddition = new ItemData { TemplateId = dummyItemTemplate2.Id };
        firstAddition.Variables.Amount = 1;

        var secondAddition = new ItemData { TemplateId = dummyItemTemplate2.Id };
        secondAddition.Variables.Amount = 1;

        var mockTemplateAccessor = new Mock<ITemplateAccessor>();
        mockTemplateAccessor
            .Setup(x => x.Get<ItemTemplate>(It.IsAny<int>()))
            .Returns((int id) => id == 100 ? dummyItemTemplate1 : dummyItemTemplate2);

        var succeeded = inventory.CreateTransaction(mockTemplateAccessor.Object)
            .RemoveItems(new[] { itemToRemove })
            .AddItems(new[] { firstAddition, secondAddition })
            .ApplyChanges();

        Assert.False(succeeded);
        Assert.Single(inventory.Items);
        Assert.Equal(dummyItemTemplate1.Id, inventory.Items[0].TemplateId);
        Assert.Equal(1, inventory.Items[0].Variables.Amount);
    }

    [Fact]
    public void should_rollback_removals_for_non_amounted_items_when_an_addition_fails()
    {
        var dummyItemTemplate1 = new ItemTemplate { Id = 100 };
        var dummyItemTemplate2 = new ItemTemplate { Id = 200 };
        var dummyItemTemplate3 = new ItemTemplate { Id = 300 };

        var itemInInventory = new ItemData { TemplateId = dummyItemTemplate1.Id };
        itemInInventory.Variables.Amount = 1;

        var inventory = new Inventory
        {
            Items = new List<ItemData> { itemInInventory.Clone() },
            Variables = new InventoryVariables()
        };
        inventory.Variables[OpenRpg.Items.Types.InventoryVariableTypes.MaxSlots] = 2;

        var itemToRemove = new ItemData { TemplateId = dummyItemTemplate1.Id };
        itemToRemove.Variables.Amount = 1;
        var firstAddition = new ItemData { TemplateId = dummyItemTemplate2.Id };
        var secondAddition = new ItemData { TemplateId = dummyItemTemplate3.Id };

        var mockTemplateAccessor = new Mock<ITemplateAccessor>();
        mockTemplateAccessor
            .Setup(x => x.Get<ItemTemplate>(It.IsAny<int>()))
            .Returns((int id) => id switch
            {
                100 => dummyItemTemplate1,
                200 => dummyItemTemplate2,
                _ => dummyItemTemplate3
            });

        var succeeded = inventory.CreateTransaction(mockTemplateAccessor.Object)
            .RemoveItems(new[] { itemToRemove })
            .AddItems(new[] { firstAddition, secondAddition })
            .ApplyChanges();

        Assert.False(succeeded);
        Assert.Single(inventory.Items);
        Assert.Equal(dummyItemTemplate1.Id, inventory.Items[0].TemplateId);
        Assert.Equal(1, inventory.Items[0].Variables.Amount);
    }
}

using System.Collections.Generic;
using System.Linq;
using OpenRpg.Items;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventory;
using OpenRpg.Items.Templates;
using Xunit;

namespace OpenRpg.UnitTests.Items;

public class InventoryTransactionTests
{
    [Fact]
    public void should_carry_out_transaction_successfully()
    {
        var dummyItemTemplate1 = new DefaultItemTemplate() { Id = 123 };
        var dummyItemTemplate2 = new DefaultItemTemplate() { Id = 345 };
        var dummyItemTemplate3 = new DefaultItemTemplate() { Id = 567 };
        var dummyItemTemplate4 = new DefaultItemTemplate() { Id = 789 };

        var itemToRemove1 = new DefaultItem() { Template = dummyItemTemplate1 };
        itemToRemove1.Variables.Amount(5);

        var itemToRemove2 = new DefaultItem() { Template = dummyItemTemplate2 };
        itemToRemove2.Variables.Amount(2);
        
        var itemToAdd1 = new DefaultItem() { Template = dummyItemTemplate3 };
        itemToAdd1.Variables.Amount(1);

        var itemToAdd2 = new DefaultItem() { Template = dummyItemTemplate4 };
        itemToAdd2.Variables.Amount(10);

        var inventory = new DefaultInventory(new[] { itemToRemove1.Clone(), itemToRemove2.Clone() });
        var succeeeded = inventory.CreateTransaction()
            .RemoveItems(new[] { itemToRemove1, itemToRemove2 })
            .AddItems(new[] { itemToAdd1, itemToAdd2 })
            .ApplyChanges();

        Assert.True(succeeeded);
        Assert.Equal(2, inventory.Items.Count);
        Assert.False(inventory.HasItem(dummyItemTemplate1));
        Assert.False(inventory.HasItem(dummyItemTemplate2));

        Assert.True(inventory.HasItem((dummyItemTemplate3)));
        var item1 = inventory.Items.Single(x => x.Template.Id == itemToAdd1.Template.Id);
        Assert.Equal(itemToAdd1.Variables.Amount(), item1.Variables.Amount());

        Assert.True(inventory.HasItem((dummyItemTemplate4)));
        var item2 = inventory.Items.Single(x => x.Template.Id == itemToAdd2.Template.Id);
        Assert.Equal(itemToAdd2.Variables.Amount(), item2.Variables.Amount());
    }
}
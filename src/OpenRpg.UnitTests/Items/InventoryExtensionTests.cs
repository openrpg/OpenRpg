using System;
using System.Collections.Generic;
using Moq;
using OpenRpg.Items;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventories;
using OpenRpg.Items.Templates;
using Xunit;

namespace OpenRpg.UnitTests.Items;

public class InventoryExtensionTests
{
    [Fact]
    public void should_tell_if_inventory_has_item_of_type()
    {
        var expectedItemTypeId = 1;
        var inventory = new Inventory();
        inventory.Items.Add(new ItemData() { TemplateId = expectedItemTypeId });

        var hasExpectedItem = inventory.HasItem(expectedItemTypeId);
        Assert.True(hasExpectedItem);

        var doesntHaveUnexpectedItem = inventory.HasItem(2);
        Assert.False(doesntHaveUnexpectedItem);
    }
    
    [Theory]
    [InlineData(5,5,true)]
    [InlineData(5,0,true)]
    [InlineData(10,5,true)]
    [InlineData(5,10,false)]
    public void should_correctly_return_if_it_has_items_with_amounts(int startingAmount, int requestAmount, bool expected)
    {
        var existingItemTemplate = new ItemTemplate { Id = 1 };
        var existingItem = new ItemData() { TemplateId = existingItemTemplate.Id };
        existingItem.Variables.Amount(startingAmount);
        
        var inventory = new Inventory();
        inventory.Items.Add(existingItem);

        var itemToCheck = new ItemData() { TemplateId = existingItemTemplate.Id };
        itemToCheck.Variables.Amount(requestAmount);
        var actualThroughAgnosticMethod = inventory.HasItem(itemToCheck);
        var actualFromDirectMethod = inventory.HasItem(existingItemTemplate.Id, requestAmount);

        Assert.Equal(expected, actualThroughAgnosticMethod);
        Assert.Equal(expected, actualFromDirectMethod);
    }
}
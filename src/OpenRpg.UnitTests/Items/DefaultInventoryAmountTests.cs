using System;
using System.Linq;
using Moq;
using OpenRpg.Core.Templates;
using OpenRpg.Items;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventories;
using OpenRpg.Items.Templates;
using Xunit;

namespace OpenRpg.UnitTests.Items;

public class DefaultInventoryAmountTests
{
    [Fact]
    public void should_add_item_amount_when_not_existing()
    {
        var expectedAmount = 25;
        var existingItemTemplate = new ItemTemplate { Id = 1 };

        var inventory = new Inventory();
        var itemToAdd = new Item() { TemplateId = existingItemTemplate.Id };
        itemToAdd.Variables.Amount(25);
        var isItemAdded = inventory.AttemptAddItem(itemToAdd, existingItemTemplate);

        Assert.True(isItemAdded);
        Assert.Contains(inventory.Items, x => x.TemplateId == existingItemTemplate.Id);
        Assert.Equal(expectedAmount, inventory.Items.First().Variables.Amount());
    }
    
    [Fact]
    public void should_add_item_amount_when_existing()
    {
        var expectedAmount = 35;
        var existingItemTemplate = new ItemTemplate { Id = 1 };
        var existingItem = new Item() { TemplateId = existingItemTemplate.Id };
        existingItem.Variables.Amount(10);
        
        var inventory = new Inventory();
        inventory.Items.Add(existingItem);
        
        var itemToAdd = new Item() { TemplateId = existingItemTemplate.Id };
        itemToAdd.Variables.Amount(25);
        var isItemAdded = inventory.AttemptAddItem(itemToAdd, existingItemTemplate);

        Assert.True(isItemAdded);
        Assert.Contains(inventory.Items, x => x.TemplateId == existingItem.TemplateId);
        Assert.Equal(expectedAmount, inventory.Items.First().Variables.Amount());
    }
    
    [Fact]
    public void should_add_item_amount_as_new_item_when_stack_limit_met()
    {
        var existingItemTemplate = new ItemTemplate { Id = 1 };
        existingItemTemplate.Variables.MaxStacks(12);
        
        var existingItem = new Item() { TemplateId = existingItemTemplate.Id };
        existingItem.Variables.Amount(10);
        
        var inventory = new Inventory();
        inventory.Items.Add(existingItem);
        
        var itemToAdd = new Item() { TemplateId = existingItemTemplate.Id };
        itemToAdd.Variables.Amount(5);
        var isItemAdded = inventory.AttemptAddItem(itemToAdd, existingItemTemplate);

        Assert.True(isItemAdded);
        Assert.Equal(2, inventory.Items.Count);
        Assert.Equal(12, existingItem.Variables.Amount());
        Assert.Equal(3, inventory.Items.First(x => x != existingItem).Variables.Amount());
    }
    
    [Fact]
    public void should_add_item_amount_when_existing_across_multiple_existing_stacks()
    {
        var existingItemTemplate = new ItemTemplate { Id = 1 };
        existingItemTemplate.Variables.MaxStacks(10);
        
        var existingItem1 = new Item() { TemplateId = existingItemTemplate.Id };
        existingItem1.Variables.Amount(8);
        
        var existingItem2 = new Item() { TemplateId = existingItemTemplate.Id };
        existingItem2.Variables.Amount(8);
        
        var inventory = new Inventory();
        inventory.Items.Add(existingItem1);
        inventory.Items.Add(existingItem2);
        
        var itemToAdd = new Item() { TemplateId = existingItemTemplate.Id };
        itemToAdd.Variables.Amount(5);
        var isItemAdded = inventory.AttemptAddItem(itemToAdd, existingItemTemplate);

        Assert.True(isItemAdded);
        Assert.Equal(3, inventory.Items.Count);
        Assert.Equal(10, existingItem1.Variables.Amount());
        Assert.Equal(10, existingItem2.Variables.Amount());
        Assert.Equal(1, inventory.Items.First(x => x != existingItem1 && x != existingItem2).Variables.Amount());
    }
    
    [Fact]
    public void should_not_add_item_amount_when_existing_across_multiple_existing_stacks_if_slots_constrained()
    {
        var existingItemTemplate = new ItemTemplate { Id = 1 };
        existingItemTemplate.Variables.MaxStacks(10);
        
        var existingItem1 = new Item() { TemplateId = existingItemTemplate.Id };
        existingItem1.Variables.Amount(8);
        
        var existingItem2 = new Item() { TemplateId = existingItemTemplate.Id };
        existingItem2.Variables.Amount(8);
        
        var inventory = new Inventory();
        inventory.Variables.MaxSlots(2);
        inventory.Items.Add(existingItem1);
        inventory.Items.Add(existingItem2);
        
        var itemToAdd = new Item() { TemplateId = existingItemTemplate.Id };
        itemToAdd.Variables.Amount(5);
        var isItemAdded = inventory.AttemptAddItem(itemToAdd, existingItemTemplate);

        Assert.False(isItemAdded);
        Assert.Equal(2, inventory.Items.Count);
        Assert.Equal(8, existingItem1.Variables.Amount());
        Assert.Equal(8, existingItem2.Variables.Amount());
    }
    
    [Fact]
    public void should_remove_item_amount_when_existing()
    {
        var expectedAmount = 75;
        var existingItemTemplate = new ItemTemplate { Id = 1 };
        var existingItem = new Item() { TemplateId = existingItemTemplate.Id };
        existingItem.Variables.Amount(100);
        
        var inventory = new Inventory();
        inventory.Items.Add(existingItem);

        var itemToRemove = new Item() { TemplateId = existingItemTemplate.Id };
        itemToRemove.Variables.Amount(25);
        var isItemRemoved = inventory.AttemptRemoveItem(itemToRemove);

        Assert.True(isItemRemoved);
        Assert.Contains(inventory.Items, x => x.TemplateId == existingItem.TemplateId);
        Assert.Equal(expectedAmount, inventory.Items.First().Variables.Amount());
    }

    [Fact]
    public void should_remove_item_when_existing_amount_matched()
    {
        var existingItemTemplate = new ItemTemplate { Id = 1 };
        var existingItem = new Item() {TemplateId = existingItemTemplate.Id };
        existingItem.Variables.Amount(100);
        
        var inventory = new Inventory();
        inventory.Items.Add(existingItem);

        var isItemRemoved = inventory.AttemptRemoveItem(existingItem);

        Assert.True(isItemRemoved);
        Assert.Empty(inventory.Items);
    }
    
    [Fact]
    public void should_remove_multiple_when_existing_amount_exceeds_single_stack()
    {
        var existingItemTemplate = new ItemTemplate { Id = 1 };
        var existingItem1 = new Item() { TemplateId = existingItemTemplate.Id };
        existingItem1.Variables.Amount(5);
        
        var existingItem2 = new Item() { TemplateId = existingItemTemplate.Id };
        existingItem2.Variables.Amount(5);
        
        var inventory = new Inventory();
        inventory.Items.Add(existingItem1);
        inventory.Items.Add(existingItem2);

        var itemToRemove = new Item() { TemplateId = existingItemTemplate.Id };
        itemToRemove.Variables.Amount(10);
        var isItemRemoved = inventory.AttemptRemoveItem(itemToRemove);

        Assert.True(isItemRemoved);
        Assert.Empty(inventory.Items);
    }
    
    [Fact]
    public void should_remove_and_alter_multiple_when_existing_amount_exceeds_single_stack()
    {
        var existingItemTemplate = new ItemTemplate { Id = 1 };
        var existingItem1 = new Item() { TemplateId = existingItemTemplate.Id };
        existingItem1.Variables.Amount(5);
        
        var existingItem2 = new Item() { TemplateId = existingItemTemplate.Id };
        existingItem2.Variables.Amount(7);
        
        var inventory = new Inventory();
        inventory.Items.Add(existingItem1);
        inventory.Items.Add(existingItem2);

        var itemToRemove = new Item() { TemplateId = existingItemTemplate.Id };
        itemToRemove.Variables.Amount(10);
        var isItemRemoved = inventory.AttemptRemoveItem(itemToRemove);

        Assert.True(isItemRemoved);
        Assert.Equal(1, inventory.Items.Count);
        Assert.Equal(2, inventory.Items.First().Variables.Amount());
    }
    
}
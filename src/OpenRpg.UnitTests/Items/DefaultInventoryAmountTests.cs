using System;
using System.Linq;
using OpenRpg.Items;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventory;
using OpenRpg.Items.Templates;
using Xunit;

namespace OpenRpg.UnitTests.Items;

public class DefaultInventoryAmountTests
{
    [Theory]
    [InlineData(5,5,true)]
    [InlineData(5,0,true)]
    [InlineData(10,5,true)]
    [InlineData(5,10,false)]
    public void should_correctly_return_if_it_has_items_with_amounts(int startingAmount, int requestAmount, bool expected)
    {
        var existingItemTemplate = new DefaultItemTemplate { Id = 1 };
        var existingItem = new DefaultItem() { UniqueId = Guid.Empty, ItemTemplate = existingItemTemplate };
        existingItem.Variables.Amount(startingAmount);
        
        var inventory = new DefaultInventory();
        inventory.InternalItems.Add(existingItem);

        var itemToCheck = new DefaultItem() { UniqueId = Guid.Empty, ItemTemplate = existingItemTemplate };
        itemToCheck.Variables.Amount(requestAmount);
        var actual = inventory.HasItem(itemToCheck);

        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void should_add_item_amount_when_not_existing()
    {
        var expectedAmount = 25;
        var existingItemTemplate = new DefaultItemTemplate { Id = 1 };
        
        var inventory = new DefaultInventory();
        var itemToAdd = new DefaultItem() { UniqueId = Guid.Empty, ItemTemplate = existingItemTemplate };
        itemToAdd.Variables.Amount(25);
        var isItemAdded = inventory.AddItem(itemToAdd);

        Assert.True(isItemAdded);
        Assert.Contains(inventory.InternalItems, x => x.ItemTemplate.Id == existingItemTemplate.Id);
        Assert.Equal(expectedAmount, inventory.Items.First().Variables.Amount());
    }
    
    [Fact]
    public void should_add_item_amount_when_existing()
    {
        var expectedAmount = 35;
        var existingItemTemplate = new DefaultItemTemplate { Id = 1 };
        var existingItem = new DefaultItem() { UniqueId = Guid.Empty, ItemTemplate = existingItemTemplate };
        existingItem.Variables.Amount(10);
        
        var inventory = new DefaultInventory();
        inventory.InternalItems.Add(existingItem);
        
        var itemToAdd = new DefaultItem() { UniqueId = Guid.Empty, ItemTemplate = existingItemTemplate };
        itemToAdd.Variables.Amount(25);
        var isItemAdded = inventory.AddItem(itemToAdd);

        Assert.True(isItemAdded);
        Assert.Contains(inventory.InternalItems, x => x.ItemTemplate.Id == existingItem.ItemTemplate.Id);
        Assert.Equal(expectedAmount, inventory.Items.First().Variables.Amount());
    }
    
    [Fact]
    public void should_add_item_amount_as_new_item_when_stack_limit_met()
    {
        var existingItemTemplate = new DefaultItemTemplate { Id = 1 };
        existingItemTemplate.Variables.MaxStacks(12);
        
        var existingItem = new DefaultItem() { UniqueId = Guid.NewGuid(), ItemTemplate = existingItemTemplate };
        existingItem.Variables.Amount(10);
        
        var inventory = new DefaultInventory();
        inventory.InternalItems.Add(existingItem);
        
        var itemToAdd = new DefaultItem() { UniqueId = Guid.Empty, ItemTemplate = existingItemTemplate };
        itemToAdd.Variables.Amount(5);
        var isItemAdded = inventory.AddItem(itemToAdd);

        Assert.True(isItemAdded);
        Assert.Equal(2, inventory.InternalItems.Count);
        Assert.Equal(12, existingItem.Variables.Amount());
        Assert.Equal(3, inventory.Items.First(x => x != existingItem).Variables.Amount());
    }
    
    [Fact]
    public void should_add_item_amount_when_existing_across_multiple_existing_stacks()
    {
        var existingItemTemplate = new DefaultItemTemplate { Id = 1 };
        existingItemTemplate.Variables.MaxStacks(10);
        
        var existingItem1 = new DefaultItem() { ItemTemplate = existingItemTemplate };
        existingItem1.Variables.Amount(8);
        
        var existingItem2 = new DefaultItem() { ItemTemplate = existingItemTemplate };
        existingItem2.Variables.Amount(8);
        
        var inventory = new DefaultInventory();
        inventory.InternalItems.Add(existingItem1);
        inventory.InternalItems.Add(existingItem2);
        
        var itemToAdd = new DefaultItem() { ItemTemplate = existingItemTemplate };
        itemToAdd.Variables.Amount(5);
        var isItemAdded = inventory.AddItem(itemToAdd);

        Assert.True(isItemAdded);
        Assert.Equal(3, inventory.InternalItems.Count);
        Assert.Equal(10, existingItem1.Variables.Amount());
        Assert.Equal(10, existingItem2.Variables.Amount());
        Assert.Equal(1, inventory.Items.First(x => x != existingItem1 && x != existingItem2).Variables.Amount());
    }
    
    [Fact]
    public void should_not_add_item_amount_when_existing_across_multiple_existing_stacks_if_slots_constrained()
    {
        var existingItemTemplate = new DefaultItemTemplate { Id = 1 };
        existingItemTemplate.Variables.MaxStacks(10);
        
        var existingItem1 = new DefaultItem() { ItemTemplate = existingItemTemplate };
        existingItem1.Variables.Amount(8);
        
        var existingItem2 = new DefaultItem() { ItemTemplate = existingItemTemplate };
        existingItem2.Variables.Amount(8);
        
        var inventory = new DefaultInventory();
        inventory.Variables.MaxSlots(2);
        inventory.InternalItems.Add(existingItem1);
        inventory.InternalItems.Add(existingItem2);
        
        var itemToAdd = new DefaultItem() { ItemTemplate = existingItemTemplate };
        itemToAdd.Variables.Amount(5);
        var isItemAdded = inventory.AddItem(itemToAdd);

        Assert.False(isItemAdded);
        Assert.Equal(2, inventory.InternalItems.Count);
        Assert.Equal(8, existingItem1.Variables.Amount());
        Assert.Equal(8, existingItem2.Variables.Amount());
    }
    
    [Fact]
    public void should_remove_item_amount_when_existing()
    {
        var expectedAmount = 75;
        var existingItemTemplate = new DefaultItemTemplate { Id = 1 };
        var existingItem = new DefaultItem() { UniqueId = Guid.Empty, ItemTemplate = existingItemTemplate };
        existingItem.Variables.Amount(100);
        
        var inventory = new DefaultInventory();
        inventory.InternalItems.Add(existingItem);

        var itemToRemove = new DefaultItem() { UniqueId = Guid.Empty, ItemTemplate = existingItemTemplate };
        itemToRemove.Variables.Amount(25);
        var isItemRemoved = inventory.RemoveItem(itemToRemove);

        Assert.True(isItemRemoved);
        Assert.Contains(inventory.InternalItems, x => x.ItemTemplate.Id == existingItem.ItemTemplate.Id);
        Assert.Equal(expectedAmount, inventory.Items.First().Variables.Amount());
    }

    [Fact]
    public void should_remove_item_when_existing_amount_matched()
    {
        var existingItemTemplate = new DefaultItemTemplate { Id = 1 };
        var existingItem = new DefaultItem() { UniqueId = Guid.Empty, ItemTemplate = existingItemTemplate };
        existingItem.Variables.Amount(100);
        
        var inventory = new DefaultInventory();
        inventory.InternalItems.Add(existingItem);

        var isItemRemoved = inventory.RemoveItem(existingItem);

        Assert.True(isItemRemoved);
        Assert.Equal(0, inventory.Items.Count);
    }
    
    [Fact]
    public void should_remove_multiple_when_existing_amount_exceeds_single_stack()
    {
        var existingItemTemplate = new DefaultItemTemplate { Id = 1 };
        var existingItem1 = new DefaultItem() { ItemTemplate = existingItemTemplate };
        existingItem1.Variables.Amount(5);
        
        var existingItem2 = new DefaultItem() { ItemTemplate = existingItemTemplate };
        existingItem2.Variables.Amount(5);
        
        var inventory = new DefaultInventory();
        inventory.InternalItems.Add(existingItem1);
        inventory.InternalItems.Add(existingItem2);

        var itemToRemove = new DefaultItem() { ItemTemplate = existingItemTemplate };
        itemToRemove.Variables.Amount(10);
        var isItemRemoved = inventory.RemoveItem(itemToRemove);

        Assert.True(isItemRemoved);
        Assert.Equal(0, inventory.Items.Count);
    }
    
    [Fact]
    public void should_remove_and_alter_multiple_when_existing_amount_exceeds_single_stack()
    {
        var existingItemTemplate = new DefaultItemTemplate { Id = 1 };
        var existingItem1 = new DefaultItem() { ItemTemplate = existingItemTemplate };
        existingItem1.Variables.Amount(5);
        
        var existingItem2 = new DefaultItem() { ItemTemplate = existingItemTemplate };
        existingItem2.Variables.Amount(7);
        
        var inventory = new DefaultInventory();
        inventory.InternalItems.Add(existingItem1);
        inventory.InternalItems.Add(existingItem2);

        var itemToRemove = new DefaultItem() { ItemTemplate = existingItemTemplate };
        itemToRemove.Variables.Amount(10);
        var isItemRemoved = inventory.RemoveItem(itemToRemove);

        Assert.True(isItemRemoved);
        Assert.Equal(1, inventory.Items.Count);
        Assert.Equal(2, inventory.Items.First().Variables.Amount());
    }
    
}
using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Items;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventory;
using OpenRpg.Items.Templates;
using OpenRpg.Items.TradeSkills;
using OpenRpg.Items.TradeSkills.Crafting;
using OpenRpg.Items.TradeSkills.Extensions;
using Xunit;

namespace OpenRpg.UnitTests.TradeSkills;

public class TradeSkillInventoryExtensionsTests
{
    [Fact]
    public void should_return_true_when_inventory_has_items_for_crafting()
    {
        var dummyItemTemplate1 = new DefaultItemTemplate() { Id = 123 };
        var dummyItemTemplate2 = new DefaultItemTemplate() { Id = 345 };

        var inputItem1 = new TradeSkillItemEntry() { Template = dummyItemTemplate1 };
        inputItem1.Variables.Amount(5);

        var inputItem2 = new TradeSkillItemEntry() { Template = dummyItemTemplate2 };
        inputItem2.Variables.Amount(2);

        var inputItems = new List<TradeSkillItemEntry>() { inputItem1, inputItem2 };
        
        var craftingTemplate = new ItemCraftingTemplate() { Id = 1, InputItems = inputItems, OutputItems = new List<TradeSkillItemEntry>() };

        var itemInInventory1 = new DefaultItem() { Template = dummyItemTemplate1 };
        itemInInventory1.Variables.Amount(5);
        
        var itemInInventory2 = new DefaultItem() { Template = dummyItemTemplate2 };
        itemInInventory2.Variables.Amount(2);

        var inventory = new DefaultInventory(new[] { itemInInventory1, itemInInventory2 });

        var hasItems = inventory.HasItemsRequiredFor(craftingTemplate);
        Assert.True(hasItems);
    }
    
    [Fact]
    public void should_return_false_when_inventory_missing_items_for_crafting()
    {
        var dummyItemTemplate1 = new DefaultItemTemplate() { Id = 123 };
        var dummyItemTemplate2 = new DefaultItemTemplate() { Id = 345 };

        var inputItem1 = new TradeSkillItemEntry() { Template = dummyItemTemplate1 };
        inputItem1.Variables.Amount(5);

        var inputItem2 = new TradeSkillItemEntry() { Template = dummyItemTemplate2 };
        inputItem2.Variables.Amount(2);

        var inputItems = new List<TradeSkillItemEntry>() { inputItem1, inputItem2 };
        
        var craftingTemplate = new ItemCraftingTemplate() { Id = 1, InputItems = inputItems, OutputItems = new List<TradeSkillItemEntry>() };

        var itemInInventory1 = new DefaultItem() { Template = dummyItemTemplate1 };
        itemInInventory1.Variables.Amount(5);

        var inventory = new DefaultInventory(new[] { itemInInventory1 });

        var hasItems = inventory.HasItemsRequiredFor(craftingTemplate);
        Assert.False(hasItems);
    }
    
    [Fact]
    public void should_craft_item()
    {
        var dummyItemTemplate1 = new DefaultItemTemplate() { Id = 123 };
        var dummyItemTemplate2 = new DefaultItemTemplate() { Id = 345 };
        var dummyItemTemplate3 = new DefaultItemTemplate() { Id = 567 };
        var dummyItemTemplate4 = new DefaultItemTemplate() { Id = 789 };

        var inputItem1 = new TradeSkillItemEntry() { Template = dummyItemTemplate1 };
        inputItem1.Variables.Amount(5);

        var inputItem2 = new TradeSkillItemEntry() { Template = dummyItemTemplate2 };
        inputItem2.Variables.Amount(2);

        var inputItems = new List<TradeSkillItemEntry>() { inputItem1, inputItem2 };
        
        var outputItem1 = new TradeSkillItemEntry() { Template = dummyItemTemplate3 };
        outputItem1.Variables.Amount(1);

        var outputItem2 = new TradeSkillItemEntry() { Template = dummyItemTemplate4 };
        outputItem2.Variables.Amount(10);
        
        var outputItems = new List<TradeSkillItemEntry>() { outputItem1, outputItem2 };

        var craftingTemplate = new ItemCraftingTemplate() { Id = 1, InputItems = inputItems, OutputItems = outputItems };

        var itemInInventory1 = new DefaultItem() { Template = dummyItemTemplate1 };
        itemInInventory1.Variables.Amount(5);
        
        var itemInInventory2 = new DefaultItem() { Template = dummyItemTemplate2 };
        itemInInventory2.Variables.Amount(2);

        var inventory = new DefaultInventory(new[] { itemInInventory1, itemInInventory2 });

        var hasItems = inventory.HasItemsRequiredFor(craftingTemplate);
        Assert.True(hasItems);

        var hasCrafted = craftingTemplate.CraftFrom(inventory);
        Assert.True(hasCrafted);
        Assert.Equal(2, inventory.Items.Count);
        Assert.False(inventory.HasItem(dummyItemTemplate1));
        Assert.False(inventory.HasItem(dummyItemTemplate2));

        Assert.True(inventory.HasItem((dummyItemTemplate3)));
        var craftedItem1 = inventory.Items.Single(x => x.Template.Id == dummyItemTemplate3.Id);
        Assert.Equal(outputItem1.Variables.Amount(), craftedItem1.Variables.Amount());

        Assert.True(inventory.HasItem((dummyItemTemplate4)));
        var craftedItem2 = inventory.Items.Single(x => x.Template.Id == dummyItemTemplate4.Id);
        Assert.Equal(outputItem2.Variables.Amount(), craftedItem2.Variables.Amount());
    }
}
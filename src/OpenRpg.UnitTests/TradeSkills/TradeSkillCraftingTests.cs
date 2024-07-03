using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using OpenRpg.Core.Templates;
using OpenRpg.Items;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventories;
using OpenRpg.Items.Templates;
using OpenRpg.Items.TradeSkills;
using OpenRpg.Items.TradeSkills.Extensions;
using OpenRpg.Items.TradeSkills.Templates;
using Xunit;

namespace OpenRpg.UnitTests.TradeSkills;

public class TradeSkillInventoryExtensionsTests
{
    [Fact]
    public void should_return_true_when_inventory_has_items_for_crafting()
    {
        var dummyItemTemplate1 = new ItemTemplate() { Id = 123 };
        var dummyItemTemplate2 = new ItemTemplate() { Id = 345 };

        var inputItem1 = new TradeSkillItemEntry() { TemplateId = dummyItemTemplate1.Id };
        inputItem1.Variables.Amount(5);

        var inputItem2 = new TradeSkillItemEntry() { TemplateId = dummyItemTemplate2.Id };
        inputItem2.Variables.Amount(2);

        var inputItems = new List<TradeSkillItemEntry>() { inputItem1, inputItem2 };
        
        var craftingTemplate = new ItemCraftingTemplate() { Id = 1, InputItems = inputItems, OutputItems = new List<TradeSkillItemEntry>() };

        var itemInInventory1 = new Item() { TemplateId = dummyItemTemplate1.Id };
        itemInInventory1.Variables.Amount(5);
        
        var itemInInventory2 = new Item() { TemplateId = dummyItemTemplate2.Id };
        itemInInventory2.Variables.Amount(2);

        var inventory = new Inventory()
        {
            Items = new List<Item> { itemInInventory1, itemInInventory2 }
        };

        var hasItems = inventory.HasItemsRequiredFor(craftingTemplate);
        Assert.True(hasItems);
    }
    
    [Fact]
    public void should_return_false_when_inventory_missing_items_for_crafting()
    {
        var dummyItemTemplate1 = new ItemTemplate() { Id = 123 };
        var dummyItemTemplate2 = new ItemTemplate() { Id = 345 };

        var inputItem1 = new TradeSkillItemEntry() { TemplateId = dummyItemTemplate1.Id };
        inputItem1.Variables.Amount(5);

        var inputItem2 = new TradeSkillItemEntry() { TemplateId = dummyItemTemplate2.Id };
        inputItem2.Variables.Amount(2);

        var inputItems = new List<TradeSkillItemEntry>() { inputItem1, inputItem2 };
        
        var craftingTemplate = new ItemCraftingTemplate() { Id = 1, InputItems = inputItems, OutputItems = new List<TradeSkillItemEntry>() };

        var itemInInventory1 = new Item() { TemplateId = dummyItemTemplate1.Id };
        itemInInventory1.Variables.Amount(5);

        var inventory = new Inventory()
        {
            Items = new List<Item>() { itemInInventory1 }
        };

        var hasItems = inventory.HasItemsRequiredFor(craftingTemplate);
        Assert.False(hasItems);
    }
    
    [Fact]
    public void should_craft_item()
    {
        var dummyItemTemplate1 = new ItemTemplate() { Id = 123 };
        var dummyItemTemplate2 = new ItemTemplate() { Id = 345 };
        var dummyItemTemplate3 = new ItemTemplate() { Id = 567 };
        var dummyItemTemplate4 = new ItemTemplate() { Id = 789 };

        var inputItem1 = new TradeSkillItemEntry() { TemplateId = dummyItemTemplate1.Id };
        inputItem1.Variables.Amount(5);

        var inputItem2 = new TradeSkillItemEntry() { TemplateId = dummyItemTemplate2.Id };
        inputItem2.Variables.Amount(2);

        var inputItems = new List<TradeSkillItemEntry>() { inputItem1, inputItem2 };
        
        var outputItem1 = new TradeSkillItemEntry() { TemplateId = dummyItemTemplate3.Id };
        outputItem1.Variables.Amount(1);

        var outputItem2 = new TradeSkillItemEntry() { TemplateId = dummyItemTemplate4.Id };
        outputItem2.Variables.Amount(10);
        
        var outputItems = new List<TradeSkillItemEntry>() { outputItem1, outputItem2 };

        var craftingTemplate = new ItemCraftingTemplate() { Id = 1, InputItems = inputItems, OutputItems = outputItems };

        var itemInInventory1 = new Item() { TemplateId = dummyItemTemplate1.Id };
        itemInInventory1.Variables.Amount(5);
        
        var itemInInventory2 = new Item() { TemplateId = dummyItemTemplate2.Id };
        itemInInventory2.Variables.Amount(2);

        var inventory = new Inventory()
        {
            Items = new List<Item>() { itemInInventory1, itemInInventory2 }
        };

        var hasItems = inventory.HasItemsRequiredFor(craftingTemplate);
        Assert.True(hasItems);

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
        
        var hasCrafted = craftingTemplate.CraftFrom(inventory, mockTemplateAccessor.Object);
        Assert.True(hasCrafted);
        Assert.Equal(2, inventory.Items.Count);
        Assert.False(inventory.HasItem(dummyItemTemplate1.Id));
        Assert.False(inventory.HasItem(dummyItemTemplate2.Id));

        Assert.True(inventory.HasItem((dummyItemTemplate3.Id)));
        var craftedItem1 = inventory.Items.Single(x => x.TemplateId == dummyItemTemplate3.Id);
        Assert.Equal(outputItem1.Variables.Amount(), craftedItem1.Variables.Amount());

        Assert.True(inventory.HasItem((dummyItemTemplate4.Id)));
        var craftedItem2 = inventory.Items.Single(x => x.TemplateId == dummyItemTemplate4.Id);
        Assert.Equal(outputItem2.Variables.Amount(), craftedItem2.Variables.Amount());
    }
}
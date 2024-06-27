using OpenRpg.Core.Classes;
using OpenRpg.Core.Entity.Variables;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Races;
using OpenRpg.Items.Equipment;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventory;
using OpenRpg.Items.Loot;
using Xunit;

namespace OpenRpg.UnitTests.Items;

public class ItemEntityVariableExtensionTests
{
    [Fact]
    public void should_correctly_handle_inventory_on_entity()
    {
        var entityVars = new DefaultEntityVariables();
        Assert.False(entityVars.HasInventory());
        
        var dummyInventory = new DefaultInventory();
        entityVars.Inventory(dummyInventory);
        Assert.True(entityVars.HasInventory());
        Assert.Equal(entityVars.Inventory(), dummyInventory);
    }
    
    [Fact]
    public void should_correctly_handle_equipment_on_entity()
    {
        var entityVars = new DefaultEntityVariables();
        Assert.False(entityVars.HasEquipment());
        
        var dummyEquipment = new DefaultEquipment();
        entityVars.Equipment(dummyEquipment);
        Assert.True(entityVars.HasEquipment());
        Assert.Equal(entityVars.Equipment(), dummyEquipment);
    }
    
    [Fact]
    public void should_correctly_handle_loot_table_on_entity()
    {
        var entityVars = new DefaultEntityVariables();
        Assert.False(entityVars.HasLootTable());
        
        var dummyLootTable = new DefaultLootTable();
        entityVars.LootTable(dummyLootTable);
        Assert.True(entityVars.HasLootTable());
        Assert.Equal(entityVars.LootTable(), dummyLootTable);
    }
}
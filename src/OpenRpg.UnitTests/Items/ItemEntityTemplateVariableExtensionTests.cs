using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventories;
using OpenRpg.Items.Loot;
using Xunit;

namespace OpenRpg.UnitTests.Items;

public class ItemEntityTemplateVariableExtensionTests
{
    [Fact]
    public void should_correctly_handle_equipment_on_entity_template()
    {
        var entityVars = new EntityTemplateVariables();
        Assert.False(entityVars.HasEquipment());
        
        var dummyEquipment = new Equipment();
        entityVars.Equipment = dummyEquipment;
        Assert.True(entityVars.HasEquipment());
        Assert.Equal(entityVars.Equipment, dummyEquipment);
    }
    
    [Fact]
    public void should_correctly_handle_loot_table_on_entity_template()
    {
        var entityVars = new EntityTemplateVariables();
        Assert.False(entityVars.HasLootTable());
        
        var dummyLootTable = new DefaultLootTable();
        entityVars.LootTable = dummyLootTable;
        Assert.True(entityVars.HasLootTable());
        Assert.Equal(entityVars.LootTable, dummyLootTable);
    }
}
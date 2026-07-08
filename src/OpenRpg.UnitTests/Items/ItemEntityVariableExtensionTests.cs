using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Inventories;
using OpenRpg.Items.Loot;
using OpenRpg.Items.Templates;
using Xunit;

namespace OpenRpg.UnitTests.Items;

public class ItemEntityVariableExtensionTests
{
    [Fact]
    public void should_correctly_handle_inventory_on_entity()
    {
        var entityVars = new EntityVariables();
        Assert.False(entityVars.HasInventory());
        
        var dummyInventory = new Inventory();
        entityVars.Inventory = dummyInventory;
        Assert.True(entityVars.HasInventory());
        Assert.Equal(entityVars.Inventory, dummyInventory);
    }

    [Fact]
    public void should_use_same_inventory_instance_on_subsequent_access()
    {
        var entityVars = new EntityVariables();
        var inv = entityVars.Inventory;
        inv.Items.Add(new ItemData());
        Assert.Single(entityVars.Inventory.Items);
    }

    [Fact]
    public void should_correctly_handle_equipment_on_entity()
    {
        var entityVars = new EntityVariables();
        Assert.False(entityVars.HasEquipment());
        
        var dummyEquipment = new Equipment();
        entityVars.Equipment = dummyEquipment;
        Assert.True(entityVars.HasEquipment());
        Assert.Equal(entityVars.Equipment, dummyEquipment);
    }

    [Fact]
    public void should_use_same_equipment_instance_on_subsequent_access()
    {
        var entityVars = new EntityVariables();
        var equip = entityVars.Equipment;
        equip.Variables = new OpenRpg.Items.Variables.EquipmentVariables();
        Assert.Same(equip.Variables, entityVars.Equipment.Variables);
    }
}
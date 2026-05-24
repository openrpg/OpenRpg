using OpenRpg.Genres.Scifi.Extensions;
using OpenRpg.Genres.Scifi.Variables;
using OpenRpg.Items.Variables;
using Xunit;

namespace OpenRpg.UnitTests.Scifi;

public class ShipVariableExtensionTests
{
    [Fact]
    public void should_use_same_equipment_instance_on_subsequent_access()
    {
        var vars = new ShipVariables();
        var equip = vars.Equipment;
        equip.Variables = new EquipmentVariables();
        Assert.Same(equip.Variables, vars.Equipment.Variables);
    }

    [Fact]
    public void should_use_same_inventory_instance_on_subsequent_access()
    {
        var vars = new ShipVariables();
        var inv = vars.Inventory;
        inv.Items.Add(new OpenRpg.Items.Templates.ItemData());
        Assert.Single(vars.Inventory.Items);
    }

    [Fact]
    public void should_use_same_pilot_instance_on_subsequent_access()
    {
        var vars = new ShipVariables();
        var pilot = vars.Pilot;
        pilot.NameLocaleId = "Test Pilot";
        Assert.Equal("Test Pilot", vars.Pilot.NameLocaleId);
    }
}

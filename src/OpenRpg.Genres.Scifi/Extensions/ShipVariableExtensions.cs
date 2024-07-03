using OpenRpg.Genres.Scifi.Equippables.ShipSlots;
using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Genres.Scifi.Variables;
using OpenRpg.Items.Inventories;

namespace OpenRpg.Genres.Scifi.Extensions
{
    /// <summary>
    /// This allows you to extend the underlying ship variables to add equipment or inventory responsibilities onto them
    /// </summary>
    public static class ShipVariableExtensions
    {
        public static bool HasEquipment(this ShipVariables vars) 
        { return vars.ContainsKey(ShipVariableTypes.Equipment); }
        
        public static ShipEquipment Equipment(this ShipVariables vars)
        { return vars[ShipVariableTypes.Equipment] as ShipEquipment; }

        public static void Equipment(this ShipVariables vars, ShipEquipment equipment)
        { vars[ShipVariableTypes.Equipment] = equipment; }
        
        public static bool HasInventory(this ShipVariables vars) 
        { return vars.ContainsKey(ShipVariableTypes.Inventory); }
        
        public static Inventory Inventory(this ShipVariables vars)
        { return vars[ShipVariableTypes.Inventory] as Inventory; }

        public static void Inventory(this ShipVariables vars, Inventory inventory)
        { vars[ShipVariableTypes.Inventory] = inventory; }
    }
}
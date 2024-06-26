using OpenRpg.Genres.Scifi.Equipment;
using OpenRpg.Genres.Scifi.Types;
using OpenRpg.Genres.Scifi.Variables;
using OpenRpg.Items.Inventory;

namespace OpenRpg.Genres.Scifi.Extensions
{
    /// <summary>
    /// This allows you to extend the underlying ship variables to add equipment or inventory responsibilities onto them
    /// </summary>
    public static class ShipVariableExtensions
    {
        public static bool HasEquipment(this IShipVariables vars) 
        { return vars.ContainsKey(ShipVariableTypes.Equipment); }
        
        public static IShipEquipment Equipment(this IShipVariables vars)
        { return vars[ShipVariableTypes.Equipment] as IShipEquipment; }

        public static void Equipment(this IShipVariables vars, IShipEquipment equipment)
        { vars[ShipVariableTypes.Equipment] = equipment; }
        
        public static bool HasInventory(this IShipVariables vars) 
        { return vars.ContainsKey(ShipVariableTypes.Inventory); }
        
        public static IInventory Inventory(this IShipVariables vars)
        { return vars[ShipVariableTypes.Inventory] as IInventory; }

        public static void Inventory(this IShipVariables vars, IInventory inventory)
        { vars[ShipVariableTypes.Inventory] = inventory; }
    }
}
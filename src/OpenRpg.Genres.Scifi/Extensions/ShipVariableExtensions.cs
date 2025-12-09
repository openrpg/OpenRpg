using OpenRpg.Core.Extensions;
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
        extension(ShipVariables vars)
        {
            public ShipEquipment Equipment
            {
                get => vars.GetAsOrDefault(ShipVariableTypes.Equipment, () => new ShipEquipment());
                set => vars[ShipVariableTypes.Equipment] = value;
            }
            
            public Inventory Inventory
            {
                get => vars.GetAsOrDefault(ShipVariableTypes.Inventory, () => new Inventory());
                set => vars[ShipVariableTypes.Inventory] = value;
            }
        }
        
        public static bool HasEquipment(this ShipVariables vars) 
        { return vars.ContainsKey(ShipVariableTypes.Equipment); }
        
        public static bool HasInventory(this ShipVariables vars) 
        { return vars.ContainsKey(ShipVariableTypes.Inventory); }
    }
}
using OpenRpg.Core.Extensions;
using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Inventories;
using OpenRpg.Items.Loot;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Extensions
{
    /// <summary>
    /// This allows you to extend the underlying entity to add equipment or inventory responsibilities onto them
    /// </summary>
    public static class ItemEntityVariableExtensions
    {
        public static bool HasEquipment(this EntityVariables vars) 
        { return vars.ContainsKey(ItemEntityVariableTypes.Equipment); }

        extension(EntityVariables vars)
        {
            public Equipment Equipment
            {
                get => vars.GetAsOrDefault(ItemEntityVariableTypes.Equipment, () => new Equipment());
                set => vars[ItemEntityVariableTypes.Equipment] = value;
            }
        }
        
        public static bool HasInventory(this EntityVariables vars) 
        { return vars.ContainsKey(ItemEntityVariableTypes.Inventory); }
        
        extension(EntityVariables vars)
        {
            public Inventory Inventory
            {
                get => vars.GetAsOrDefault(ItemEntityVariableTypes.Inventory, () => new Inventory());
                set => vars[ItemEntityVariableTypes.Inventory] = value;
            }
        }
    }
}
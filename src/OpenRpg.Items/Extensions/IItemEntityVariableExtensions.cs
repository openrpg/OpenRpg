using OpenRpg.Core.Variables.Entity;
using OpenRpg.Items.Equipment;
using OpenRpg.Items.Inventory;
using OpenRpg.Items.Loot;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Extensions
{
    /// <summary>
    /// This allows you to extend the underlying entity to add equipment or inventory responsibilities onto them
    /// </summary>
    public static class ItemEntityVariableExtensions
    {
        public static bool HasEquipment(this IEntityVariables vars) 
        { return vars.ContainsKey(ItemEntityVariableTypes.Equipment); }
        
        public static IEquipment Equipment(this IEntityVariables vars)
        { return vars[ItemEntityVariableTypes.Equipment] as IEquipment; }

        public static void Equipment(this IEntityVariables vars, IEquipment equipment)
        { vars[ItemEntityVariableTypes.Equipment] = equipment; }
        
        public static bool HasInventory(this IEntityVariables vars) 
        { return vars.ContainsKey(ItemEntityVariableTypes.Inventory); }
        
        public static IInventory Inventory(this IEntityVariables vars)
        { return vars[ItemEntityVariableTypes.Inventory] as IInventory; }

        public static void Inventory(this IEntityVariables vars, IInventory inventory)
        { vars[ItemEntityVariableTypes.Inventory] = inventory; }
        
        public static bool HasLootTable(this IEntityVariables vars) 
        { return vars.ContainsKey(ItemEntityVariableTypes.LootTable); }
        
        public static ILootTable LootTable(this IEntityVariables vars)
        { return vars[ItemEntityVariableTypes.LootTable] as ILootTable; }

        public static void LootTable(this IEntityVariables vars, ILootTable lootTable)
        { vars[ItemEntityVariableTypes.LootTable] = lootTable; }
    }
}
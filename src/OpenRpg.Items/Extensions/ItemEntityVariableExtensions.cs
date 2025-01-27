using OpenRpg.Core.Entity.Variables;
using OpenRpg.Core.Extensions;
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
        
        public static Equipment Equipment(this EntityVariables vars)
        { return vars.GetAs<Equipment>(ItemEntityVariableTypes.Equipment); }

        public static void Equipment(this EntityVariables vars, Equipment equipment)
        { vars[ItemEntityVariableTypes.Equipment] = equipment; }
        
        public static bool HasInventory(this EntityVariables vars) 
        { return vars.ContainsKey(ItemEntityVariableTypes.Inventory); }
        
        public static Inventory Inventory(this EntityVariables vars)
        { return vars.GetAs<Inventory>(ItemEntityVariableTypes.Inventory); }

        public static void Inventory(this EntityVariables vars, Inventory inventory)
        { vars[ItemEntityVariableTypes.Inventory] = inventory; }
        
        public static bool HasLootTable(this EntityVariables vars) 
        { return vars.ContainsKey(ItemEntityVariableTypes.LootTable); }
        
        public static ILootTable LootTable(this EntityVariables vars)
        { return vars.GetAs<ILootTable>(ItemEntityVariableTypes.LootTable); }

        public static void LootTable(this EntityVariables vars, ILootTable lootTable)
        { vars[ItemEntityVariableTypes.LootTable] = lootTable; }
    }
}
using OpenRpg.Core.Extensions;
using OpenRpg.Entities.Entity.Variables;
using OpenRpg.Items.Equippables;
using OpenRpg.Items.Loot;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Extensions;

public static class EntityTemplateVariableExtensions
{
    public static bool HasEquipment(this EntityTemplateVariables vars) 
    { return vars.ContainsKey(ItemEntityTemplateVariableTypes.Equipment); }

    extension(EntityTemplateVariables vars)
    {
        public Equipment Equipment
        {
            get => vars.GetAsOrDefaultAndSet(ItemEntityTemplateVariableTypes.Equipment, () => new Equipment());
            set => vars[ItemEntityTemplateVariableTypes.Equipment] = value;
        }
    }
    
    public static bool HasLootTable(this EntityTemplateVariables vars) 
    { return vars.ContainsKey(ItemEntityTemplateVariableTypes.LootTable); }
        
    extension(EntityTemplateVariables vars)
    {
        public ILootTable LootTable
        {
            get => vars.GetAsOrDefaultAndSet(ItemEntityTemplateVariableTypes.LootTable, () => new DefaultLootTable());
            set => vars[ItemEntityTemplateVariableTypes.LootTable] = value;
        }
    }
}
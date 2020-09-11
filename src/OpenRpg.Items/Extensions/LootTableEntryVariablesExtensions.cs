using OpenRpg.Items.Templates;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Extensions
{
    public static class LootTableEntryVariablesExtensions
    {
        public static float DropRate(this ILootTableEntryVariables variables) => variables[LootTableEntryVariableTypes.DropRate];
        public static void DropRate(this ILootTableEntryVariables variables, float value) => variables[LootTableEntryVariableTypes.DropRate] = value;
        public static bool IsUnique(this ILootTableEntryVariables variables) => variables[LootTableEntryVariableTypes.IsUnique] == 1;
        public static void IsUnique(this ILootTableEntryVariables variables, bool value) => variables[LootTableEntryVariableTypes.IsUnique] = (value ? 1 : 0);
    }
}
using OpenRpg.Items.Templates;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Extensions
{
    public static class LootTableEntryVariablesExtensions
    {
        public static float DropRate(this ILootTableEntryVariables variables) => variables[LootTableEntryVariableTypes.DropRate];
        
        /// <summary>
        /// The DropRate should be a value between 0-1 in which 0 is no drop chance, 0.5 is 50% drop chance, 1 is 100% drop chance
        /// </summary>
        /// <param name="value">The drop chance between 0-1</param>
        /// <returns>The drop chance can be as low as needed i.e a 1 in 1000 can be represented as 0.001</returns>
        public static void DropRate(this ILootTableEntryVariables variables, float value) => variables[LootTableEntryVariableTypes.DropRate] = value;
        
        public static bool IsUnique(this ILootTableEntryVariables variables) => variables[LootTableEntryVariableTypes.IsUnique] == 1;
        
        /// <summary>
        /// This indicates that the template associated with this item can only be dropped once
        /// </summary>
        /// <param name="variables"></param>
        /// <param name="value">a true or false</param>
        /// <remarks>This still needs to be rolled on so even though something is unique it may not be looted</remarks>
        public static void IsUnique(this ILootTableEntryVariables variables, bool value) => variables[LootTableEntryVariableTypes.IsUnique] = (value ? 1 : 0);
    }
}
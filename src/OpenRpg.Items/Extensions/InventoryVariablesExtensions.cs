using OpenRpg.Items.Inventory;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Extensions
{
    public static class InventoryVariablesExtensions
    {
        public static float MaxWeight(this IInventoryVariables variables) => (float)variables[InventoryVariableTypes.MaxWeight];
        public static void MaxWeight(this IInventoryVariables variables, float value) => variables[InventoryVariableTypes.MaxWeight] = value;
        public static float Weight(this IInventoryVariables variables) => (float)variables[InventoryVariableTypes.Weight];
        public static void Weight(this IInventoryVariables variables, float value) => variables[InventoryVariableTypes.Weight] = value;
        public static int MaxSlots(this IInventoryVariables variables) => (int)variables[InventoryVariableTypes.MaxSlots];
        public static void MaxSlots(this IInventoryVariables variables, int value) => variables[InventoryVariableTypes.MaxSlots] = value;
    }
}
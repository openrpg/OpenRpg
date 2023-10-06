using System;
using OpenRpg.Core.Extensions;
using OpenRpg.Items.Inventory;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Extensions
{
    public static class InventoryVariablesExtensions
    {
        public static bool HasMaxWeight(this IInventoryVariables variables) => variables.ContainsKey(InventoryVariableTypes.MaxWeight);
        public static float MaxWeight(this IInventoryVariables variables) => variables.GetFloat(InventoryVariableTypes.MaxWeight);
        public static void MaxWeight(this IInventoryVariables variables, float value) => variables[InventoryVariableTypes.MaxWeight] = value;
        public static float Weight(this IInventoryVariables variables) => variables.GetFloat(InventoryVariableTypes.Weight);
        public static void Weight(this IInventoryVariables variables, float value) => variables[InventoryVariableTypes.Weight] = value;
        public static bool HasMaxSlots(this IInventoryVariables variables) => variables.ContainsKey(InventoryVariableTypes.MaxSlots);
        public static int MaxSlots(this IInventoryVariables variables) => variables.GetInt(InventoryVariableTypes.MaxSlots);
        public static void MaxSlots(this IInventoryVariables variables, int value) => variables[InventoryVariableTypes.MaxSlots] = value;
    }
}
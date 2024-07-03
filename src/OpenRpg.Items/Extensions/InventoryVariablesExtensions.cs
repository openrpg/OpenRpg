using System;
using OpenRpg.Core.Extensions;
using OpenRpg.Items.Types;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Extensions
{
    public static class InventoryVariablesExtensions
    {
        public static bool HasMaxWeight(this InventoryVariables variables) => variables.ContainsKey(InventoryVariableTypes.MaxWeight);
        public static float MaxWeight(this InventoryVariables variables) => variables.GetFloat(InventoryVariableTypes.MaxWeight);
        public static void MaxWeight(this InventoryVariables variables, float value) => variables[InventoryVariableTypes.MaxWeight] = value;
        public static float Weight(this InventoryVariables variables) => variables.GetFloat(InventoryVariableTypes.Weight);
        public static void Weight(this InventoryVariables variables, float value) => variables[InventoryVariableTypes.Weight] = value;
        public static bool HasMaxSlots(this InventoryVariables variables) => variables.ContainsKey(InventoryVariableTypes.MaxSlots);
        public static int MaxSlots(this InventoryVariables variables) => variables.GetInt(InventoryVariableTypes.MaxSlots);
        public static void MaxSlots(this InventoryVariables variables, int value) => variables[InventoryVariableTypes.MaxSlots] = value;
    }
}
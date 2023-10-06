using System;
using System.Collections.Generic;
using OpenRpg.Core.Extensions;
using OpenRpg.Items.Types;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Extensions
{
    public static class ItemVariablesExtensions
    {
        public static IItemVariables Clone(this DefaultItemVariables itemVariables)
        {
            return new DefaultItemVariables
            {
                InternalVariables = new Dictionary<int, object>(itemVariables.InternalVariables)
            };
        }
        
        public static bool HasAmount(this IItemVariables variables)
        { return variables.ContainsKey(ItemVariableTypes.Amount); }
        
        public static int Amount(this IItemVariables variables)
        { return variables.GetIntOrDefault(ItemVariableTypes.Amount, 1); }

        public static void Amount(this IItemVariables variables, int value)
        { variables[ItemVariableTypes.Amount] = value; }

        public static bool HasWeight(this IItemVariables variables)
        { return variables.ContainsKey(ItemVariableTypes.Weight); }
        
        public static float Weight(this IItemVariables variables) => variables.GetFloat(ItemVariableTypes.Weight);
        public static void Weight(this IItemVariables variables, float value) => variables[ItemVariableTypes.Weight] = value;
    }
}
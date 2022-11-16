using System;
using System.Collections.Generic;
using OpenRpg.Items.Types;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Extensions
{
    public static class ItemVariablesExtensions
    {
        public static IItemVariables Clone(this DefaultItemVariables itemVariables)
        {
            return new DefaultItemVariables { InternalVariables = new Dictionary<int, object>(itemVariables.InternalVariables) };
        }
        
        public static int Amount(this IItemVariables variables)
        {
            var amountObject = variables.Get(DefaultItemVariableTypes.Amount);
            var amount = Convert.ToInt32(amountObject);
            return amount == 0 ? 1 : amount;
        }

        public static void Amount(this IItemVariables variables, int value) => variables[DefaultItemVariableTypes.Amount] = value;
        
        public static int Weight(this IItemVariables variables) => Convert.ToInt32(variables.Get(DefaultItemVariableTypes.Weight));
        public static void Weight(this IItemVariables variables, int value) => variables[DefaultItemVariableTypes.Weight] = value;
    }
}
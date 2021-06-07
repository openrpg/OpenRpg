using System;
using System.Collections.Generic;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Extensions
{
    public static class ItemVariablesExtensions
    {
        public static IItemVariables Clone(this DefaultItemVariables itemVariables)
        {
            return new DefaultItemVariables { InternalVariables = new Dictionary<int, object>(itemVariables.InternalVariables) };
        }
        
        public static int Amount(this IItemVariables variables) => Convert.ToInt32(variables.GetVariable(ItemVariableTypes.Amount));
        public static void Amount(this IItemVariables variables, int value) => variables[ItemVariableTypes.Amount] = value;
    }
}
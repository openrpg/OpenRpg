using System.Collections.Generic;
using OpenRpg.Core.Associations;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Utils;
using OpenRpg.Items.Types;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Extensions
{
    public static class ItemVariablesExtensions
    {
        public static ItemVariables Clone(this ItemVariables itemVariables)
        {
            return new ItemVariables
            {
                InternalVariables = new Dictionary<int, object>(itemVariables.InternalVariables)
            };
        }
        
        public static bool HasAmount(this ItemVariables variables)
        { return variables.ContainsKey(ItemVariableTypes.Amount); }
        
        extension(ItemVariables vars)
        {
            public int Amount
            {
                get => vars.GetIntOrDefault(ItemVariableTypes.Amount, 1);
                set => vars[ItemVariableTypes.Amount] = value;
            }
        }
        
        public static bool HasWeight(this ItemVariables variables)
        { return variables.ContainsKey(ItemVariableTypes.Weight); }
        
        extension(ItemVariables vars)
        {
            public float Weight
            {
                get => vars.GetFloat(ItemVariableTypes.Weight);
                set => vars[ItemVariableTypes.Weight] = value;
            }
        }
    }
}
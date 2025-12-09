using System;
using OpenRpg.Core.Extensions;
using OpenRpg.Items.Types;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Extensions
{
    public static class InventoryVariablesExtensions
    {
        public static bool HasMaxWeight(this InventoryVariables variables) => variables.ContainsKey(InventoryVariableTypes.MaxWeight);

        extension(InventoryVariables vars)
        {
            public float MaxWeight
            {
                get => vars.GetFloat(InventoryVariableTypes.MaxWeight);
                set =>  vars[InventoryVariableTypes.MaxWeight] = value;
            }
            
            public float Weight
            {
                get => vars.GetFloat(InventoryVariableTypes.Weight);
                set =>  vars[InventoryVariableTypes.Weight] = value;
            }
        }
        
        public static bool HasMaxSlots(this InventoryVariables variables) => variables.ContainsKey(InventoryVariableTypes.MaxSlots);
        
        extension(InventoryVariables vars)
        {
            public int MaxSlots
            {
                get => vars.GetInt(InventoryVariableTypes.MaxSlots);
                set =>  vars[InventoryVariableTypes.MaxSlots] = value;
            }
        }
    }
}
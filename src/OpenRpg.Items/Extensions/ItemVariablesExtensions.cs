using System.Collections.Generic;
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
        
        public static int Amount(this ItemVariables variables)
        { return variables.GetIntOrDefault(ItemVariableTypes.Amount, 1); }

        public static void Amount(this ItemVariables variables, int value)
        { variables[ItemVariableTypes.Amount] = value; }

        public static bool HasWeight(this ItemVariables variables)
        { return variables.ContainsKey(ItemVariableTypes.Weight); }
        
        public static float Weight(this ItemVariables variables) => variables.GetFloat(ItemVariableTypes.Weight);
        public static void Weight(this ItemVariables variables, float value) => variables[ItemVariableTypes.Weight] = value;
        
        public static List<int> ProceduralEffects(this ItemVariables variables) => variables.GetAsOrDefault(ItemVariableTypes.ProceduralEffects, () => new List<int>());

        public static void AddProceduralEffect(this ItemVariables variables, Association effectAssociation)
        {
        }
    }
}
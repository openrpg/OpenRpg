using OpenRpg.Core.Extensions;
using OpenRpg.Items.Types;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Extensions
{
    public static class ItemTemplateVariablesExtensions
    {
        public static int QualityType(this ItemTemplateVariables variables) => variables.GetInt(ItemTemplateVariableTypes.QualityType);
        public static void QualityType(this ItemTemplateVariables variables, int value) => variables[ItemTemplateVariableTypes.QualityType] = value;
        public static int Value(this ItemTemplateVariables variables) => variables.GetInt(ItemTemplateVariableTypes.Value);
        public static void Value(this ItemTemplateVariables variables, int value) => variables[ItemTemplateVariableTypes.Value] = value;
        public static int MaxStacks(this ItemTemplateVariables variables) => variables.GetInt(ItemTemplateVariableTypes.MaxStacks);
        public static void MaxStacks(this ItemTemplateVariables variables, int value) => variables[ItemTemplateVariableTypes.MaxStacks] = value;
        public static float Weight(this ItemTemplateVariables variables) => variables.GetFloat(ItemTemplateVariableTypes.Weight);
        public static void Weight(this ItemTemplateVariables variables, float value) => variables[ItemTemplateVariableTypes.Weight] = value;
        public static int SlotType(this ItemTemplateVariables variables) => variables.GetInt(ItemTemplateVariableTypes.SlotType);
        public static void SlotType(this ItemTemplateVariables variables, int value) => variables[ItemTemplateVariableTypes.SlotType] = value;
    }
}
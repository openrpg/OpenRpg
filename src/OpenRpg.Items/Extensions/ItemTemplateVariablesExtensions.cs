using OpenRpg.Items.Templates;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Extensions
{
    public static class ItemTemplateVariablesExtensions
    {
        public static int QualityType(this IItemTemplateVariables variables) => (int)variables[ItemTemplateVariableTypes.QualityType];
        public static void QualityType(this IItemTemplateVariables variables, int value) => variables[ItemTemplateVariableTypes.QualityType] = value;
        public static int Value(this IItemTemplateVariables variables) => (int)variables[ItemTemplateVariableTypes.Value];
        public static void Value(this IItemTemplateVariables variables, int value) => variables[ItemTemplateVariableTypes.Value] = value;
        public static int MaxStacks(this IItemTemplateVariables variables) => (int)variables[ItemTemplateVariableTypes.MaxStacks];
        public static void MaxStacks(this IItemTemplateVariables variables, int value) => variables[ItemTemplateVariableTypes.MaxStacks] = value;
        public static float Weight(this IItemTemplateVariables variables) => (float)variables[ItemTemplateVariableTypes.Weight];
        public static void Weight(this IItemTemplateVariables variables, float value) => variables[ItemTemplateVariableTypes.Weight] = value;
        public static int SlotType(this IItemTemplateVariables variables) => (int)variables[ItemTemplateVariableTypes.SlotType];
        public static void SlotType(this IItemTemplateVariables variables, int value) => variables[ItemTemplateVariableTypes.SlotType] = value;
    }
}
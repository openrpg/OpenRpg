using OpenRpg.Core.Extensions;
using OpenRpg.Items.Types;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Extensions
{
    public static class ItemTemplateVariablesExtensions
    {
        public static bool HasQualityType(this ItemTemplateVariables vars) => vars.ContainsKey(ItemTemplateVariableTypes.QualityType);
        public static bool HasValue(this ItemTemplateVariables vars) => vars.ContainsKey(ItemTemplateVariableTypes.Value);
        public static bool HasMaxStacks(this ItemTemplateVariables vars) => vars.ContainsKey(ItemTemplateVariableTypes.MaxStacks);
        public static bool HasWeight(this ItemTemplateVariables vars) => vars.ContainsKey(ItemTemplateVariableTypes.Weight);
        public static bool HasSlotType(this ItemTemplateVariables vars) => vars.ContainsKey(ItemTemplateVariableTypes.SlotType);
        
        extension(ItemTemplateVariables vars)
        {
            public int QualityType
            {
                get => vars.GetInt(ItemTemplateVariableTypes.QualityType);
                set => vars[ItemTemplateVariableTypes.QualityType] = value;
            }
            
            public int Value
            {
                get => vars.GetInt(ItemTemplateVariableTypes.Value);
                set => vars[ItemTemplateVariableTypes.Value] = value;
            }
            
            public int MaxStacks
            {
                get => vars.GetInt(ItemTemplateVariableTypes.MaxStacks);
                set => vars[ItemTemplateVariableTypes.MaxStacks] = value;
            }
            
            public int Weight
            {
                get => vars.GetInt(ItemTemplateVariableTypes.Weight);
                set => vars[ItemTemplateVariableTypes.Weight] = value;
            }
            
            public int SlotType
            {
                get => vars.GetInt(ItemTemplateVariableTypes.SlotType);
                set => vars[ItemTemplateVariableTypes.SlotType] = value;
            }
        }
    }
}
using OpenRpg.Core.Extensions;
using OpenRpg.Items.TradeSkills.Types;
using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills.Extensions;

public static class ItemTradeSkillTemplateVariableExtensions
{
    public static bool HasSkillType(this IItemTradeSkillTemplateVariables vars) 
    { return vars.ContainsKey(ItemTradeSkillTemplateVariableTypes.SkillType); }
    
    public static bool HasTimeToAction(this IItemTradeSkillTemplateVariables vars) 
    { return vars.ContainsKey(ItemTradeSkillTemplateVariableTypes.TimeToComplete); }
    
    extension(IItemTradeSkillTemplateVariables vars)
    {
        public int SkillType
        {
            get => vars.GetInt(ItemTradeSkillTemplateVariableTypes.SkillType);
            set => vars[ItemTradeSkillTemplateVariableTypes.SkillType] = value;
        }
        
        public float TimeToAction
        {
            get => vars.GetInt(ItemTradeSkillTemplateVariableTypes.TimeToComplete);
            set => vars[ItemTradeSkillTemplateVariableTypes.TimeToComplete] = value;
        }
    }
}
using OpenRpg.Core.Extensions;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;
using OpenRpg.Items.TradeSkills.Types;
using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills.Extensions
{
    public static class TradeSkillItemEntryExtensions
    {
        /// <summary>
        /// This provides a way to get an item that can be used for inventory interactions
        /// </summary>
        /// <param name="tradeSkillItemEntry"></param>
        /// <returns></returns>
        public static ItemData AsItem(this TradeSkillItemEntry tradeSkillItemEntry)
        {
            var wrapperItem = new ItemData()
            {
                TemplateId = tradeSkillItemEntry.TemplateId
            };

            if (tradeSkillItemEntry.Variables.HasAmount())
            { wrapperItem.Variables.Amount = tradeSkillItemEntry.Variables.Amount; }

            if (wrapperItem.Variables.HasWeight())
            { wrapperItem.Variables.Weight = tradeSkillItemEntry.Variables.Weight; }

            return wrapperItem;
        }
        
        public static bool HasAmount(this TradeSkillItemEntryVariables variables)
        { return variables.ContainsKey(TradeSkillItemEntryVariableTypes.Amount); }

        extension(TradeSkillItemEntryVariables vars)
        {
            public int Amount
            {
                get => vars.GetIntOrDefault(TradeSkillItemEntryVariableTypes.Amount, 1);
                set => vars[TradeSkillItemEntryVariableTypes.Amount] = value;
            }
        }

        public static bool HasWeight(this TradeSkillItemEntryVariables variables)
        { return variables.ContainsKey(TradeSkillItemEntryVariableTypes.Weight); }
        
        extension(TradeSkillItemEntryVariables vars)
        {
            public float Weight
            {
                get => vars.GetFloat(TradeSkillItemEntryVariableTypes.Weight);
                set => vars[TradeSkillItemEntryVariableTypes.Weight] = value;
            }
        }
    }
}
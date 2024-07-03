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
        public static Item AsItem(this TradeSkillItemEntry tradeSkillItemEntry)
        {
            var wrapperItem = new Item()
            {
                TemplateId = tradeSkillItemEntry.TemplateId
            };

            if (tradeSkillItemEntry.Variables.HasAmount())
            { wrapperItem.Variables.Amount(tradeSkillItemEntry.Variables.Amount()); }

            if (wrapperItem.Variables.HasWeight())
            { wrapperItem.Variables.Weight(tradeSkillItemEntry.Variables.Weight()); }

            return wrapperItem;
        }
        
        public static bool HasAmount(this TradeSkillItemEntryVariables variables)
        { return variables.ContainsKey(TradeSkillItemEntryVariableTypes.Amount); }
        
        public static int Amount(this TradeSkillItemEntryVariables variables)
        { return variables.GetIntOrDefault(TradeSkillItemEntryVariableTypes.Amount, 1); }

        public static void Amount(this TradeSkillItemEntryVariables variables, int value)
        { variables[TradeSkillItemEntryVariableTypes.Amount] = value; }

        public static bool HasWeight(this TradeSkillItemEntryVariables variables)
        { return variables.ContainsKey(TradeSkillItemEntryVariableTypes.Weight); }
        
        public static float Weight(this TradeSkillItemEntryVariables variables) => variables.GetFloat(TradeSkillItemEntryVariableTypes.Weight);
        public static void Weight(this TradeSkillItemEntryVariables variables, float value) => variables[TradeSkillItemEntryVariableTypes.Weight] = value;
    }
}
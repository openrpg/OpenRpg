using System;
using OpenRpg.Items.TradeSkills.Types;
using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills.Extensions
{
    public static class TradeSkillItemEntryExtensions
    {
        public static bool HasAmount(this TradeSkillItemEntryVariables variables)
        { return variables.ContainsKey(TradeSkillItemEntryVariableTypes.Amount); }
        
        public static int Amount(this TradeSkillItemEntryVariables variables)
        {
            var amountObject = variables.Get(TradeSkillItemEntryVariableTypes.Amount);
            var amount = Convert.ToInt32(amountObject);
            return amount == 0 ? 1 : amount;
        }

        public static void Amount(this TradeSkillItemEntryVariables variables, int value)
        { variables[TradeSkillItemEntryVariableTypes.Amount] = value; }

        public static bool HasWeight(this TradeSkillItemEntryVariables variables)
        { return variables.ContainsKey(TradeSkillItemEntryVariableTypes.Weight); }
        
        public static float Weight(this TradeSkillItemEntryVariables variables) => Convert.ToSingle(variables.Get(TradeSkillItemEntryVariableTypes.Weight));
        public static void Weight(this TradeSkillItemEntryVariables variables, float value) => variables[TradeSkillItemEntryVariableTypes.Weight] = value;
    }
}
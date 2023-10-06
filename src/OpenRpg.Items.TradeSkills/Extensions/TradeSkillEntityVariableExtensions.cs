using OpenRpg.Core.Extensions;
using OpenRpg.Core.Variables.Entity;
using OpenRpg.Items.Equipment;
using OpenRpg.Items.TradeSkills.State;
using OpenRpg.Items.TradeSkills.Types;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.TradeSkills.Extensions
{
    /// <summary>
    /// This allows you to extend the underlying entity to add trade skill responsibilities onto them
    /// </summary>
    public static class TradeSkillEntityVariableExtensions
    {
        public static bool HasTradeSkillState(this IEntityVariables vars) 
        { return vars.ContainsKey(TradeSkillEntityVariableTypes.TradeSkillState); }
        
        public static ITradeSkillState TradeSkillState(this IEntityVariables vars)
        { return vars.GetAs<ITradeSkillState>(TradeSkillEntityVariableTypes.TradeSkillState); }

        public static void TradeSkillState(this IEntityVariables vars, ITradeSkillState tradeSkillState)
        { vars[TradeSkillEntityVariableTypes.TradeSkillState] = tradeSkillState; }
    }
}
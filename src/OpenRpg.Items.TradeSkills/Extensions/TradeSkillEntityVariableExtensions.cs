using OpenRpg.Core.Entity.Variables;
using OpenRpg.Core.Extensions;
using OpenRpg.Items.TradeSkills.State;
using OpenRpg.Items.TradeSkills.Types;

namespace OpenRpg.Items.TradeSkills.Extensions
{
    /// <summary>
    /// This allows you to extend the underlying entity to add trade skill responsibilities onto them
    /// </summary>
    public static class TradeSkillEntityVariableExtensions
    {
        public static bool HasTradeSkillState(this EntityVariables vars) 
        { return vars.ContainsKey(TradeSkillEntityVariableTypes.TradeSkillState); }
        
        public static TradeSkillState TradeSkillState(this EntityVariables vars)
        { return vars.GetAs<TradeSkillState>(TradeSkillEntityVariableTypes.TradeSkillState); }

        public static void TradeSkillState(this EntityVariables vars, TradeSkillState tradeSkillState)
        { vars[TradeSkillEntityVariableTypes.TradeSkillState] = tradeSkillState; }
    }
}
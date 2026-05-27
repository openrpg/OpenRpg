using OpenRpg.Core.Extensions;
using OpenRpg.Entities.Entity.Variables;
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

        extension(EntityVariables vars)
        {
            public TradeSkillState TradeSkillState
            {
                get => vars.GetAsOrDefaultAndSet(TradeSkillEntityVariableTypes.TradeSkillState, () => new TradeSkillState());
                set => vars[TradeSkillEntityVariableTypes.TradeSkillState] = value;
            }
        }
    }
}
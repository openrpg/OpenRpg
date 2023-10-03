using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Items.TradeSkills.Types;

namespace OpenRpg.Items.TradeSkills.State
{
    public class DefaultTradeSkillState : DefaultVariables<int>, ITradeSkillState
    {
        public DefaultTradeSkillState(IDictionary<int, int> internalVariables = null) : base(TradeSkillCoreVariableTypes.TradeSkillStateVariables, internalVariables)
        {
        }
    }
}
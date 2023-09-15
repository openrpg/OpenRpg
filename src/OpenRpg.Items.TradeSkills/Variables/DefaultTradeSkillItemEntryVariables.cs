using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Items.TradeSkills.Types;

namespace OpenRpg.Items.TradeSkills.Variables
{
    public class DefaultTradeSkillItemEntryVariables : DefaultVariables<object>, TradeSkillItemEntryVariables
    {
        public DefaultTradeSkillItemEntryVariables(IDictionary<int, object> internalVariables = null) : base(TradeSkillCoreVariableTypes.TradeSkillItemEntryVariables, internalVariables)
        {
        }
    }
}
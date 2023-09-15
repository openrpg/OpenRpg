using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Items.TradeSkills.Types;

namespace OpenRpg.Items.TradeSkills.Variables
{
    public class DefaultItemTradeEntryVariables : DefaultVariables<object>, IItemTradeTradeEntryVariables
    {
        public DefaultItemTradeEntryVariables(IDictionary<int, object> internalVariables = null) : base(TradeSkillCoreVariableTypes.ItemTradeEntryVariables, internalVariables)
        {
        }
    }
}
using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.TradeSkills.Types;

namespace OpenRpg.Items.TradeSkills.Variables
{
    public class ItemTradeEntryVariables : ObjectVariables
    {
        public ItemTradeEntryVariables(IDictionary<int, object> internalVariables = null) : base(TradeSkillCoreVariableTypes.ItemTradeEntryVariables, internalVariables)
        {
        }
    }
}
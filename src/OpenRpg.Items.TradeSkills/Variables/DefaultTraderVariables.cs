using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Items.TradeSkills.Types;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.TradeSkills.Variables
{
    public class DefaultTraderVariables : DefaultVariables<object>, ITraderVariables
    {
        public DefaultTraderVariables(IDictionary<int, object> internalVariables = null) : base(TradeSkillCoreVariableTypes.TraderVariables, internalVariables)
        {
        }
    }
}
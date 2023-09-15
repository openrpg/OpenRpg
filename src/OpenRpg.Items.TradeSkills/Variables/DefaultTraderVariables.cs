using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.TradeSkills.Types;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.TradeSkills.Variables
{
    public class DefaultTraderVariables : ObjectVariables, ITraderVariables
    {
        public DefaultTraderVariables(IDictionary<int, object> internalVariables = null) : base(TradeSkillCoreVariableTypes.TraderVariables, internalVariables)
        {
        }
    }
}
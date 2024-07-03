using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.TradeSkills.Types;

namespace OpenRpg.Items.TradeSkills.Variables
{
    public class TraderVariables : ObjectVariables
    {
        public TraderVariables(IDictionary<int, object> internalVariables = null) : base(TradeSkillCoreVariableTypes.TraderVariables, internalVariables)
        {
        }
    }
}
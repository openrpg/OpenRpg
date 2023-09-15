using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.TradeSkills.Types;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.TradeSkills.Variables
{
    public class DefaultTradeSkillTemplateVariables : ObjectVariables, ITradeSkillTemplateVariables
    {
        public DefaultTradeSkillTemplateVariables(IDictionary<int, object> internalVariables = null) : base(TradeSkillCoreVariableTypes.TradeSkillTemplateVariables, internalVariables)
        {
        }
    }
}
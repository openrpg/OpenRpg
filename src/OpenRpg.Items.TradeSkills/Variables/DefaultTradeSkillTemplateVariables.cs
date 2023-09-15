using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Items.TradeSkills.Types;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.TradeSkills.Variables
{
    public class DefaultTradeSkillTemplateVariables : DefaultVariables<object>, ITradeSkillTemplateVariables
    {
        public DefaultTradeSkillTemplateVariables(IDictionary<int, object> internalVariables = null) : base(TradeSkillCoreVariableTypes.TradeSkillTemplateVariables, internalVariables)
        {
        }
    }
}
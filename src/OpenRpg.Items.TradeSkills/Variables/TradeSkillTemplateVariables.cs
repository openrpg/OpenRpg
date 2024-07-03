using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.TradeSkills.Types;

namespace OpenRpg.Items.TradeSkills.Variables
{
    public class TradeSkillTemplateVariables : ObjectVariables
    {
        public TradeSkillTemplateVariables(IDictionary<int, object> internalVariables = null) : base(TradeSkillCoreVariableTypes.TradeSkillTemplateVariables, internalVariables)
        {
        }
    }
}
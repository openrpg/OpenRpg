using System.Collections.Generic;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.TradeSkills.Types;

namespace OpenRpg.Items.TradeSkills.Variables
{
    public class ItemGatheringTemplateVariables : ObjectVariables, ITemplateVariables
    {
        public ItemGatheringTemplateVariables(IDictionary<int, object> internalVariables = null) : base(TradeSkillCoreVariableTypes.ItemGatheringTemplateVariables, internalVariables)
        {
        }
    }
}
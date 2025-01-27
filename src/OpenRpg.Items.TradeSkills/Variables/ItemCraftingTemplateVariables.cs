using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.TradeSkills.Types;

namespace OpenRpg.Items.TradeSkills.Variables
{
    public class ItemCraftingTemplateVariables : ObjectVariables
    {
        public ItemCraftingTemplateVariables(IDictionary<int, object> internalVariables = null) : base(TradeSkillCoreVariableTypes.ItemCraftingTemplateVariables, internalVariables)
        {
        }
    }
}
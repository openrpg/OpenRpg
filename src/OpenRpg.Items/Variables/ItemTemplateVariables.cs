using System.Collections.Generic;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Variables
{
    public class ItemTemplateVariables : ObjectVariables, ITemplateVariables
    {
        public ItemTemplateVariables(IDictionary<int, object> internalVariables = null) : base(ItemCoreVariableTypes.ItemTemplateVariables, internalVariables)
        {
        }
    }
}
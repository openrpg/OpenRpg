using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Variables
{
    public class ItemTemplateVariables : ObjectVariables
    {
        public ItemTemplateVariables(IDictionary<int, object> internalVariables = null) : base(ItemCoreVariableTypes.ItemTemplateVariables, internalVariables)
        {
        }
    }
}
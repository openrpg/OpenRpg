using System.Collections.Generic;
using OpenRpg.Core.Templates.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Variables
{
    public class ItemVariables : ObjectVariables, ITemplateDataVariables
    {
        public ItemVariables(IDictionary<int, object> internalVariables = null) : base(ItemCoreVariableTypes.ItemVariables, internalVariables)
        {
        }
    }
}
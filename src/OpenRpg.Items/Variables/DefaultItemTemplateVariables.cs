using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Variables
{
    public class DefaultItemTemplateVariables : ObjectVariables, IItemTemplateVariables
    {
        public DefaultItemTemplateVariables(IDictionary<int, object> internalVariables = null) : base(ItemCoreVariableTypes.ItemTemplateVariables, internalVariables)
        {
        }
    }
}
using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Variables
{
    public class DefaultItemTemplateVariables : DefaultVariables<object>, IItemTemplateVariables
    {
        public DefaultItemTemplateVariables(IDictionary<int, object> internalVariables = null) : base(ItemCoreVariableTypes.ItemTemplateVariables, internalVariables)
        {
        }
    }
}
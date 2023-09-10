using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Variables
{
    public class DefaultItemVariables : DefaultVariables<object>, IItemVariables
    {
        public DefaultItemVariables(IDictionary<int, object> internalVariables = null) : base(ItemCoreVariableTypes.ItemVariables, internalVariables)
        {
        }
    }
}
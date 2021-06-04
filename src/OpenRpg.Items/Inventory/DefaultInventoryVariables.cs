using System.Collections.Generic;
using OpenRpg.Core.Variables;

namespace OpenRpg.Items.Inventory
{
    public class DefaultInventoryVariables : DefaultVariables<object>, IInventoryVariables
    {
        public DefaultInventoryVariables(IDictionary<int, object> internalVariables = null) : base(internalVariables)
        {
        }
    }
}
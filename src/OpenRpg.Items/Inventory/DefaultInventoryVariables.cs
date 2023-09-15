using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Inventory
{
    public class DefaultInventoryVariables : ObjectVariables, IInventoryVariables
    {
        public DefaultInventoryVariables(IDictionary<int, object> internalVariables = null) : base(ItemCoreVariableTypes.InventoryVariables, internalVariables)
        {
        }
    }
}
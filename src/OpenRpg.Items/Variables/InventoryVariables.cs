using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Variables
{
    public class InventoryVariables : ObjectVariables
    {
        public InventoryVariables(IDictionary<int, object> internalVariables = null) : base(ItemCoreVariableTypes.InventoryVariables, internalVariables)
        {
        }
    }
}
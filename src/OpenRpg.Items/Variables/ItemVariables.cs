using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Variables
{
    public class ItemVariables : ObjectVariables
    {
        public ItemVariables(IDictionary<int, object> internalVariables = null) : base(ItemCoreVariableTypes.ItemVariables, internalVariables)
        {
        }
    }
}
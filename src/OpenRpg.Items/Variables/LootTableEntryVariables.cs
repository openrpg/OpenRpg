using System.Collections.Generic;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Variables
{
    public class LootTableEntryVariables : ObjectVariables
    {
        public LootTableEntryVariables(IDictionary<int, object> internalVariables = null) : base(ItemCoreVariableTypes.LootTableEntryVariables, internalVariables)
        {
        }
    }
}
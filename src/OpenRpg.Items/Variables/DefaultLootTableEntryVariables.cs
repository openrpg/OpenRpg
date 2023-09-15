using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Variables
{
    public class DefaultLootTableEntryVariables : ObjectVariables, ILootTableEntryVariables
    {
        public DefaultLootTableEntryVariables(IDictionary<int, object> internalVariables = null) : base(ItemCoreVariableTypes.LootTableEntryVariables, internalVariables)
        {
        }
    }
}
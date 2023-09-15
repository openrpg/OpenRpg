using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Variables
{
    public class DefaultLootTableEntryVariables : DefaultVariables<object>, ILootTableEntryVariables
    {
        public DefaultLootTableEntryVariables(IDictionary<int, object> internalVariables = null) : base(ItemCoreVariableTypes.LootTableEntryVariables, internalVariables)
        {
        }
    }
}
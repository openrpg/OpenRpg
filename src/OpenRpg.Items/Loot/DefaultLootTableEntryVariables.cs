using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Loot
{
    public class DefaultLootTableEntryVariables : DefaultVariables<object>, ILootTableEntryVariables
    {
        public DefaultLootTableEntryVariables(IDictionary<int, object> internalVariables = null) : base(ItemVariableTypes.LootTableEntryVariables, internalVariables)
        {
        }
    }
}
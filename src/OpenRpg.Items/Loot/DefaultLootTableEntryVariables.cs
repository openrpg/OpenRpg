using System.Collections.Generic;
using OpenRpg.Core.Variables;
using OpenRpg.Items.Loot;

namespace OpenRpg.Items.Templates
{
    public class DefaultLootTableEntryVariables : DefaultVariables<object>, ILootTableEntryVariables
    {
        public DefaultLootTableEntryVariables(IDictionary<int, object> internalVariables = null) : base(internalVariables)
        {
        }
    }
}
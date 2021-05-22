using System.Collections.Generic;
using OpenRpg.Core.Requirements;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Loot
{
    public class DefaultLootTableEntry : ILootTableEntry
    {
        public IEnumerable<Requirement> Requirements { get; set; } = new Requirement[0];
        public ILootTableEntryVariables Variables { get; set; } = new DefaultLootTableEntryVariables();
        public IItem Item { get; set; }
    }
}
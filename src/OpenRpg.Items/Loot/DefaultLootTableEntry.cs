using System;
using System.Collections.Generic;
using OpenRpg.Core.Requirements;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Loot
{
    public class DefaultLootTableEntry : ILootTableEntry
    {
        public IEnumerable<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
        public ILootTableEntryVariables Variables { get; set; } = new DefaultLootTableEntryVariables();
        public IItemTemplateInstance Item { get; set; }
    }
}
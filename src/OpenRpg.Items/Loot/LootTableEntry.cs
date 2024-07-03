using System;
using System.Collections.Generic;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Variables.General;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Loot
{
    public class LootTableEntry : IHasRequirements, IHasVariables<LootTableEntryVariables>
    {
        public IReadOnlyCollection<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();
        public LootTableEntryVariables Variables { get; set; } = new LootTableEntryVariables();
        public Item Item { get; set; }
    }
}
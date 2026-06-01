using System.Collections.Generic;

namespace OpenRpg.Items.Loot;

public class LootTableData
{
    public ICollection<LootTableEntry> AvailableLoot { get; set; } = new List<LootTableEntry>();
}
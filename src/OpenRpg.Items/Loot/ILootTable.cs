using System.Collections.Generic;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Loot
{
    public interface ILootTable
    {
        ICollection<LootTableEntry> AvailableLoot { get; }
        IEnumerable<ItemData> GetLoot();
        IEnumerable<LootTableEntry> GetRandomLootEntries();
    }
}
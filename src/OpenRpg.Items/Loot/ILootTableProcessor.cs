using System.Collections.Generic;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Loot
{
    public interface ILootTableProcessor
    {
        IEnumerable<ItemData> GetLoot(LootTableData lootTable);
        IEnumerable<LootTableEntry> GetRandomLootEntries(LootTableData lootTable);
    }
}
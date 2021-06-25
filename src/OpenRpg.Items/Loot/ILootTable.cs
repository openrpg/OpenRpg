using System.Collections.Generic;

namespace OpenRpg.Items.Loot
{
    public interface ILootTable
    {
        ICollection<ILootTableEntry> AvailableLoot { get; }
        IEnumerable<IItem> GetLoot();
        IEnumerable<ILootTableEntry> GetRandomLootEntries();
    }
}
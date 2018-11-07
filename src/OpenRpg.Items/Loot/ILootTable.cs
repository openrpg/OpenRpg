using System.Collections.Generic;

namespace OpenRpg.Items.Loot
{
    public interface ILootTable
    {
        IReadOnlyCollection<ILootTableEntry> AvailableLoot { get; }

        IEnumerable<IItem> GetLoot();
    }
}
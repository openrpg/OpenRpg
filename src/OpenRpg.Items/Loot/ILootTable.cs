using System.Collections.Generic;
using OpenRpg.Items.Templates;

namespace OpenRpg.Items.Loot
{
    public interface ILootTable
    {
        ICollection<ILootTableEntry> AvailableLoot { get; }
        IEnumerable<IItemTemplateInstance> GetLoot();
        IEnumerable<ILootTableEntry> GetRandomLootEntries();
    }
}
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Utils;
using OpenRpg.Items.Extensions;

namespace OpenRpg.Items.Loot
{
    public class DefaultLootTable : ILootTable
    {
        public IRandomizer Randomizer { get; set; }
        public IReadOnlyCollection<ILootTableEntry> AvailableLoot { get; set; }

        public DefaultLootTable(){}
        
        public DefaultLootTable(IReadOnlyCollection<ILootTableEntry> availableLoot, IRandomizer randomizer)
        {
            Randomizer = randomizer;
            AvailableLoot = availableLoot;
        }
        
        public ILootTableEntry GetNextLootEntry(List<ILootTableEntry> currentlyTaken)
        {
            var randomChance = Randomizer.Random(0, 100);
            var applicableLoot = AvailableLoot.Where(x => x.Variables.DropRate() >= randomChance && !currentlyTaken.Contains(x));
            var randomEntry = applicableLoot.TakeRandom(Randomizer);
            return randomEntry;
        }

        public IEnumerable<IItem> GetLoot()
        {
            var lootList = new List<ILootTableEntry>();

            while (true)
            {
                if (lootList.Count == AvailableLoot.Count)
                { yield break; }
                
                var drop = GetNextLootEntry(lootList);

                if(drop.Variables.IsUnique())
                { lootList.Add(drop); }

                yield return drop.Item;
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Utils;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Loot
{
    public class DefaultLootTable : ILootTable
    {
        public IRandomizer Randomizer { get; set; }
        public ICollection<LootTableEntry> AvailableLoot { get; set; } = new List<LootTableEntry>();

        public DefaultLootTable(){}
        
        public DefaultLootTable(ICollection<LootTableEntry> availableLoot, IRandomizer randomizer)
        {
            Randomizer = randomizer;
            AvailableLoot = availableLoot;
        }
        
        public IEnumerable<ItemData> GetLoot()
        { return GetRandomLootEntries().Select(x => x.ItemData.Clone()); }
        
        public IEnumerable<LootTableEntry> GetRandomLootEntries()
        {
            var uniqueItems = new List<ItemData>();

            foreach (var loot in AvailableLoot)
            {
                var randomChance = Randomizer.Random(0f, 1f);
                if (!(loot.Variables.DropRate() >= randomChance)) { continue; }
                
                if (loot.Variables.ContainsKey(LootTableEntryVariableTypes.IsUnique) && loot.Variables.IsUnique())
                {
                    if(uniqueItems.Contains(loot.ItemData))
                    { continue; }
                    
                    uniqueItems.Add(loot.ItemData);
                }

                yield return loot;
            }
        }
    }
}
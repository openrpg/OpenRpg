using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Utils;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Types;

namespace OpenRpg.Items.Loot
{
    public class DefaultLootTableProcessor : ILootTableProcessor
    {
        public IRandomizer Randomizer { get; set; }
        
        public DefaultLootTableProcessor(){}
        
        public DefaultLootTableProcessor(IRandomizer randomizer)
        { Randomizer = randomizer; }
        
        public IEnumerable<ItemData> GetLoot(LootTableData lootTable)
        { return GetRandomLootEntries(lootTable).Select(x => x.ItemData.Clone()); }
        
        public IEnumerable<LootTableEntry> GetRandomLootEntries(LootTableData lootTable)
        {
            var uniqueItems = new List<ItemData>();

            foreach (var loot in lootTable.AvailableLoot)
            {
                var randomChance = Randomizer.Random(0f, 1f);
                if (!(loot.Variables.DropRate >= randomChance)) { continue; }
                
                if (loot.Variables.ContainsKey(LootTableEntryVariableTypes.IsUnique) && loot.Variables.IsUnique)
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
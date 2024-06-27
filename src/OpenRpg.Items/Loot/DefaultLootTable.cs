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
        public ICollection<ILootTableEntry> AvailableLoot { get; set; } = new List<ILootTableEntry>();

        public DefaultLootTable(){}
        
        public DefaultLootTable(ICollection<ILootTableEntry> availableLoot, IRandomizer randomizer)
        {
            Randomizer = randomizer;
            AvailableLoot = availableLoot;
        }
        
        public IEnumerable<IItemTemplateInstance> GetLoot()
        { return GetRandomLootEntries().Select(x => x.Item.Clone()); }
        
        public IEnumerable<ILootTableEntry> GetRandomLootEntries()
        {
            var uniqueItems = new List<IItemTemplateInstance>();

            foreach (var loot in AvailableLoot)
            {
                var randomChance = Randomizer.Random(0f, 1f);
                if (!(loot.Variables.DropRate() >= randomChance)) { continue; }
                
                if (loot.Variables.ContainsKey(LootTableEntryVariableTypes.IsUnique) && loot.Variables.IsUnique())
                {
                    if(uniqueItems.Contains(loot.Item))
                    { continue; }
                    
                    uniqueItems.Add(loot.Item);
                }

                yield return loot;
            }
        }
    }
}
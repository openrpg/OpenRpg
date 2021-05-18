using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Extensions;
using OpenRpg.Core.Utils;
using OpenRpg.Items.Extensions;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Types;

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
        
        public IEnumerable<IItem> GetLoot()
        {
            var uniqueItems = new List<IItemTemplate>();

            foreach (var loot in AvailableLoot)
            {
                var randomChance = Randomizer.Random(0f, 1f);
                if (!(loot.Variables.DropRate() >= randomChance)) { continue; }
                
                if (loot.Variables.HasVariable(LootTableEntryVariableTypes.IsUnique) && loot.Variables.IsUnique())
                {
                    if(uniqueItems.Contains(loot.Item.ItemTemplate))
                    { continue; }
                    uniqueItems.Add(loot.Item.ItemTemplate);
                }

                yield return (loot.Item as DefaultItem).Clone();
            }
        }
    }
}
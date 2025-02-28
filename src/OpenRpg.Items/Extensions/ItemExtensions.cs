using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Requirements;
using OpenRpg.Items.Loot;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Extensions
{
    public static class ItemExtensions
    {
        /// <summary>
        /// Clones the item copying the modifications and variables
        /// </summary>
        /// <param name="itemData">The item to clone</param>
        public static ItemData Clone(this ItemData itemData)
        {
            return new ItemData
            {
                TemplateId = itemData.TemplateId,
                Variables = itemData.Variables.Clone(),
                Modifications = new List<ItemModificationData>(itemData.Modifications.ToArray())
            };
        }
        
        /// <summary>
        /// This will clone an Item
        /// </summary>
        /// <param name="item">The item to clone</param>
        public static Item Clone(this Item item)
        {
            var instanceClone = item.Data.Clone();
            return new Item() { Data = instanceClone, Template = item.Template };
        }

        public static LootTableEntry GenerateLootTableEntry(this ItemData itemData, float dropRate = 1, bool isUnique = false, IReadOnlyCollection<Requirement> requirements = null)
        {
            var variables = new LootTableEntryVariables();
            variables.DropRate(dropRate);
            variables.IsUnique(isUnique);

            return new LootTableEntry
            {
                ItemData = itemData,
                Requirements = requirements ?? Array.Empty<Requirement>(),
                Variables = variables
            };
        }
    }
}
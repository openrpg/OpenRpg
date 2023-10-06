using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Modifications;
using OpenRpg.Core.Requirements;
using OpenRpg.Items.Loot;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Extensions
{
    public static class ItemExtensions
    {
        /// <summary>
        /// This will attempt to make a default clone of the item based off the default implementation
        /// </summary>
        /// <param name="item">The item to clone</param>
        public static IItem Clone(this DefaultItem item)
        {
            return new DefaultItem
            {
                Template = item.Template,
                Variables = (item.Variables as DefaultItemVariables).Clone(),
                Modifications = new List<IModificationTemplate>(item.Modifications)
            };
        }

        public static ILootTableEntry GenerateLootTableEntry(this IItem item, float dropRate = 1, bool isUnique = false, IEnumerable<Requirement> requirements = null)
        {
            var variables = new DefaultLootTableEntryVariables();
            variables.DropRate(dropRate);
            variables.IsUnique(isUnique);

            return new DefaultLootTableEntry
            {
                Item = item,
                Requirements = requirements ?? Array.Empty<Requirement>(),
                Variables = variables
            };
        }
        
        public static IEnumerable<Effect> GetItemEffects(this IItem item)
        {
            if(!item.Modifications.Any())
            { return item.Template.Effects; }
         
            return item.Template.Effects.Union(item.Modifications.SelectMany(x => x.Effects));
        }
    }
}
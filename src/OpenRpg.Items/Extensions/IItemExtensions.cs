using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Items.Loot;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Variables;

namespace OpenRpg.Items.Extensions
{
    public static class ItemExtensions
    {
        /// <summary>
        /// This will attempt to make a default clone of the item based off the default implementation
        /// </summary>
        /// <param name="itemTemplate">The item to clone</param>
        public static IItemTemplateInstance Clone(this IItemTemplateInstance itemTemplate)
        {
            return new DefaultItemTemplateInstance
            {
                TemplateId = itemTemplate.TemplateId,
                Variables = (itemTemplate.Variables as DefaultItemVariables).Clone(),
                Modifications = new List<IItemModificationTemplate>(itemTemplate.Modifications)
            };
        }

        public static ILootTableEntry GenerateLootTableEntry(this IItemTemplateInstance item, float dropRate = 1, bool isUnique = false, IEnumerable<Requirement> requirements = null)
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
            if(!item.Instance.Modifications.Any())
            { return item.Template.Effects; }
         
            return item.Template.Effects.Union(item.Instance.Modifications.SelectMany(x => x.Effects));
        }
    }
}
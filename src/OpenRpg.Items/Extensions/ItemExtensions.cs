using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Core.Common;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Variables;
using OpenRpg.Core.Variables.General;
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
        /// <param name="item">The item to clone</param>
        public static Item Clone(this Item item)
        {
            return new Item
            {
                TemplateId = item.TemplateId,
                Variables = item.Variables.Clone(),
                Modifications = new List<ItemModificationTemplate>(item.Modifications.ToArray())
            };
        }
        
        /// <summary>
        /// This will clone an ItemView
        /// </summary>
        /// <param name="item">The item to clone</param>
        public static ItemView Clone(this ItemView item)
        {
            var instanceClone = item.Instance.Clone();
            return new ItemView() { Instance = instanceClone, Template = item.Template };
        }

        public static LootTableEntry GenerateLootTableEntry(this Item item, float dropRate = 1, bool isUnique = false, IReadOnlyCollection<Requirement> requirements = null)
        {
            var variables = new LootTableEntryVariables();
            variables.DropRate(dropRate);
            variables.IsUnique(isUnique);

            return new LootTableEntry
            {
                Item = item,
                Requirements = requirements ?? Array.Empty<Requirement>(),
                Variables = variables
            };
        }
        
        public static IReadOnlyCollection<Effect> GetItemEffects(this Item item, ItemTemplate itemTemplate)
        {
            if(!item.Modifications.Any())
            { return itemTemplate.Effects; }
         
            return itemTemplate.Effects.Union(item.Modifications.SelectMany(x => x.Effects)).ToArray();
        }
        
        public static IReadOnlyCollection<Effect> GetItemEffects(this ItemView item)
        {
            if(!item.Instance.Modifications.Any())
            { return item.Template.Effects; }
         
            return item.Template.Effects.Union(item.Instance.Modifications.SelectMany(x => x.Effects)).ToArray();
        }
        
        
        public static void TestMethod<T>(this T instance) 
            where T : class, IHasVariables<IVariables<object>>, IHasLocaleDescription
        {
            instance.Variables[100] = 100;

            new ItemTemplate().TestMethod();
        }
    }
}
using System.Collections.Generic;
using OpenRpg.Core.Templates;
using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills.Templates
{
    public class ItemCraftingTemplate : ITemplate<ItemCraftingTemplateVariables>
    {
        /// <summary>
        /// The Id for this template
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name locale id for the crafting
        /// </summary>
        public string NameLocaleId { get; set; }
        
        /// <summary>
        /// The description locale id
        /// </summary>
        public string DescriptionLocaleId { get; set; }

        /// <summary>
        /// Variables for this template
        /// </summary>
        public ItemCraftingTemplateVariables Variables { get; set; } = new ItemCraftingTemplateVariables();
        
        /// <summary>
        /// The items required to craft this template
        /// </summary>
        public List<TradeSkillItemEntry> InputItems { get; set; } = new List<TradeSkillItemEntry>();
        
        /// <summary>
        /// The items output from this template
        /// </summary>
        public List<TradeSkillItemEntry> OutputItems { get; set; } = new List<TradeSkillItemEntry>();
    }
}
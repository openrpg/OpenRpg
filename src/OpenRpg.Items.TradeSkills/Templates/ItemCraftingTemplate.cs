using System.Collections.Generic;

namespace OpenRpg.Items.TradeSkills.Templates
{
    public class ItemCraftingTemplate : TradeSkillTemplate
    {
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
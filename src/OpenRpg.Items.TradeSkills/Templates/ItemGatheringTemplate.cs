using System.Collections.Generic;

namespace OpenRpg.Items.TradeSkills.Templates
{
    public class ItemGatheringTemplate : TradeSkillTemplate
    {
        /// <summary>
        /// The items output from this template
        /// </summary>
        public List<TradeSkillItemEntry> OutputItems { get; set; } = new List<TradeSkillItemEntry>();
    }
}

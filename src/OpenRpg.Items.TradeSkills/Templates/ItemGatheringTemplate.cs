using System.Collections.Generic;
using OpenRpg.Core.Templates;
using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills.Templates
{
    public class ItemGatheringTemplate : ITemplate<ItemGatheringTemplateVariables>
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public string NameLocaleId { get; set; }
        
        /// <inheritdoc />
        public string DescriptionLocaleId { get; set; }

        /// <inheritdoc />
        public ItemGatheringTemplateVariables Variables { get; set; } = new ItemGatheringTemplateVariables();
        
        /// <summary>
        /// The items output from this template
        /// </summary>
        public List<TradeSkillItemEntry> OutputItems { get; set; } = new List<TradeSkillItemEntry>();
    }
}

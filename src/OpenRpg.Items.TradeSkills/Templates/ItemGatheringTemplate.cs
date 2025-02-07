using System;
using System.Collections.Generic;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Requirements;
using OpenRpg.Items.TradeSkills.Variables;

namespace OpenRpg.Items.TradeSkills.Templates
{
    public class ItemGatheringTemplate : ITemplate<ItemGatheringTemplateVariables>, ITradeSkillData, IHasRequirements
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public string NameLocaleId { get; }
        
        /// <inheritdoc />
        public string DescriptionLocaleId { get; }
        
        /// <inheritdoc />
        public float TimeToComplete { get; set; } = 1.0f;

        /// <inheritdoc />
        public int SkillType { get; set; }
    
        /// <inheritdoc />
        public int SkillDifficulty { get; set; }

        /// <inheritdoc />
        public IReadOnlyCollection<Requirement> Requirements { get; set; } = Array.Empty<Requirement>();

        /// <inheritdoc />
        public ItemGatheringTemplateVariables Variables { get; set; } = new ItemGatheringTemplateVariables();
        
        /// <summary>
        /// The items output from this template
        /// </summary>
        public List<TradeSkillItemEntry> OutputItems { get; set; } = new List<TradeSkillItemEntry>();
    }
}

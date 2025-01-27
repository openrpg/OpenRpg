using System.Collections.Generic;
using OpenRpg.Core.Requirements;
using OpenRpg.Demos.Infrastructure.Lookups;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.TradeSkills;
using OpenRpg.Items.TradeSkills.Extensions;
using OpenRpg.Items.TradeSkills.Templates;

namespace OpenRpg.Demos.Infrastructure.Data
{
    public class GatheringTemplateDataGenerator : IDataGenerator<ItemGatheringTemplate>
    {
        public IEnumerable<ItemGatheringTemplate> GenerateData()
        {
            return new []
            {
                MakeCopperOreGatheringTemplate(),
                MakeIronOreGatheringTemplate(),
                MakeOakLogGatheringTemplate(),
            };
        }

        public ItemGatheringTemplate MakeCopperOreGatheringTemplate()
        {
            var itemEntry = new TradeSkillItemEntry() { TemplateId = ItemTemplateLookups.CopperOre };
            itemEntry.Variables.Amount(1);
        
            return new ItemGatheringTemplate()
            {
                Id = ItemGatheringTemplateLookups.CopperOre,
                SkillType = FantasyTradeSkillTypes.Mining,
                SkillDifficulty = 5,
                TimeToComplete = 1.0f,
                OutputItems = new List<TradeSkillItemEntry>() { itemEntry }
            };
        }
        
        public ItemGatheringTemplate MakeIronOreGatheringTemplate()
        {
            var itemEntry = new TradeSkillItemEntry() { TemplateId = ItemTemplateLookups.IronOre };
            itemEntry.Variables.Amount(1);
        
            return new ItemGatheringTemplate()
            {
                Id = ItemGatheringTemplateLookups.IronOre,
                SkillType = FantasyTradeSkillTypes.Mining,
                SkillDifficulty = 15,
                TimeToComplete = 1.0f,
                OutputItems = new List<TradeSkillItemEntry>() { itemEntry },
                Requirements =  new []
                {
                    new Requirement { RequirementType = FantasyRequirementTypes.TradeSkillRequirement, AssociatedId = FantasyTradeSkillTypes.Mining, AssociatedValue = 10 }
                },
            };
        }

        public ItemGatheringTemplate MakeOakLogGatheringTemplate()
        {
            var itemEntry = new TradeSkillItemEntry() { TemplateId = ItemTemplateLookups.OakLog };
            itemEntry.Variables.Amount(1);
        
            return new ItemGatheringTemplate()
            {
                Id = ItemGatheringTemplateLookups.OakLog,
                SkillType = FantasyTradeSkillTypes.Logging,
                SkillDifficulty = 0,
                TimeToComplete = 1.0f,
                OutputItems = new List<TradeSkillItemEntry>() { itemEntry }
            };
        }

    }
}
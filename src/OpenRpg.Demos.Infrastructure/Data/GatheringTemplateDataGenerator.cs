using System.Collections.Generic;
using OpenRpg.Core.Associations;
using OpenRpg.Core.Requirements;
using OpenRpg.Demos.Infrastructure.Lookups;
using OpenRpg.Entities.Extensions;
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
            itemEntry.Variables.Amount = 1;
        
            var template = new ItemGatheringTemplate()
            {
                Id = ItemGatheringTemplateLookups.CopperOre,
                OutputItems = new List<TradeSkillItemEntry>() { itemEntry }
            };
            template.Variables.SkillType = FantasyTradeSkillTypes.Mining;
            template.Variables.TimeToAction = 1.0f;
            return template;
        }
        
        public ItemGatheringTemplate MakeIronOreGatheringTemplate()
        {
            var itemEntry = new TradeSkillItemEntry() { TemplateId = ItemTemplateLookups.IronOre };
            itemEntry.Variables.Amount = 1;
        
            var template = new ItemGatheringTemplate()
            {
                Id = ItemGatheringTemplateLookups.IronOre,
                OutputItems = new List<TradeSkillItemEntry>() { itemEntry }
            };
            template.Variables.SkillType = FantasyTradeSkillTypes.Mining;
            template.Variables.TimeToAction = 1.0f;
            template.Variables.Requirements =
            [
                new Requirement
                {
                    RequirementType = FantasyRequirementTypes.TradeSkillRequirement,
                    Association = new Association(FantasyTradeSkillTypes.Mining, 10)
                }
            ];
            return template;
        }

        public ItemGatheringTemplate MakeOakLogGatheringTemplate()
        {
            var itemEntry = new TradeSkillItemEntry() { TemplateId = ItemTemplateLookups.OakLog };
            itemEntry.Variables.Amount = 1;
        
            var template = new ItemGatheringTemplate()
            {
                Id = ItemGatheringTemplateLookups.OakLog,
                OutputItems = new List<TradeSkillItemEntry>() { itemEntry }
            };
            
            template.Variables.SkillType = FantasyTradeSkillTypes.Logging;
            template.Variables.TimeToAction = 1.0f;

            return template;
        }

    }
}
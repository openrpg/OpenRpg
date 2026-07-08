using System.Collections.Generic;
using OpenRpg.Core.Associations;
using OpenRpg.Core.Requirements;
using OpenRpg.Demos.Infrastructure.Lookups;
using OpenRpg.Entities.Extensions;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.TradeSkills;
using OpenRpg.Items.TradeSkills.Extensions;
using OpenRpg.Items.TradeSkills.Templates;

namespace OpenRpg.Demos.Infrastructure.Data;

public class CraftingTemplateDataGenerator : IDataGenerator<ItemCraftingTemplate>
{
    public IEnumerable<ItemCraftingTemplate> GenerateData()
    {
        return new []
        {
            MakeCopperIngotCraftingTemplate(),
            MakeCopperSwordCraftingTemplate()
        };
    }

    public ItemCraftingTemplate MakeCopperIngotCraftingTemplate()
    {
        var inputItemEntry = new TradeSkillItemEntry() { TemplateId = ItemTemplateLookups.CopperOre };
        inputItemEntry.Variables.Amount = 5;

        var outputItemEntry = new TradeSkillItemEntry() { TemplateId = ItemTemplateLookups.CopperIngot };
        outputItemEntry.Variables.Amount = 1;
        
        var template = new ItemCraftingTemplate()
        {
            Id = ItemCraftingTemplateLookups.CopperIngot,
            InputItems = new List<TradeSkillItemEntry>() { inputItemEntry },
            OutputItems = new List<TradeSkillItemEntry>() { outputItemEntry },
        };
        template.Variables.SkillType = FantasyTradeSkillTypes.Smelting;
        template.Variables.TimeToAction = 2.0f;
        return template;
    }

    public ItemCraftingTemplate MakeCopperSwordCraftingTemplate()
    {
        var inputItem1Entry = new TradeSkillItemEntry() { TemplateId = ItemTemplateLookups.CopperIngot };
        inputItem1Entry.Variables.Amount = 2;
        var inputItem2Entry = new TradeSkillItemEntry() { TemplateId = ItemTemplateLookups.OakLog };
        inputItem2Entry.Variables.Amount = 1;

        var outputItemEntry = new TradeSkillItemEntry() { TemplateId = ItemTemplateLookups.CopperSword };
        outputItemEntry.Variables.Amount = 1;
        
        var craftingTemplate = new ItemCraftingTemplate()
        {
            Id = ItemCraftingTemplateLookups.CopperSword,
            InputItems = new List<TradeSkillItemEntry>() { inputItem1Entry, inputItem2Entry },
            OutputItems = new List<TradeSkillItemEntry>() { outputItemEntry }
        };
        craftingTemplate.Variables.SkillType = FantasyTradeSkillTypes.Smithing;
        craftingTemplate.Variables.TimeToAction = 2.0f;
        craftingTemplate.Variables.Requirements =
        [
            new Requirement
            {
                RequirementType = FantasyRequirementTypes.TradeSkillRequirement,
                Association = new Association(FantasyTradeSkillTypes.Smithing, 10)
            }
        ];
        
        craftingTemplate.Variables.Requirements = new[]
        {
            new Requirement
            {
                RequirementType = FantasyRequirementTypes.TradeSkillRequirement,
                Association = new Association(FantasyTradeSkillTypes.Smithing, 5)
            }
        };
        return craftingTemplate;
    }
}
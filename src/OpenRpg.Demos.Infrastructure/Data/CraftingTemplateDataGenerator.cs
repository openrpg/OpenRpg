using System.Collections.Generic;
using OpenRpg.Core.Utils;
using OpenRpg.Demos.Infrastructure.Lookups;
using OpenRpg.Entities.Requirements;
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
        inputItemEntry.Variables.Amount(5);

        var outputItemEntry = new TradeSkillItemEntry() { TemplateId = ItemTemplateLookups.CopperIngot };
        outputItemEntry.Variables.Amount(1);
        
        return new ItemCraftingTemplate()
        {
            Id = ItemCraftingTemplateLookups.CopperIngot,
            SkillType = FantasyCraftingTradeSkillTypes.Smelting,
            SkillDifficulty = 0,
            TimeToComplete = 2.0f,
            InputItems = new List<TradeSkillItemEntry>() { inputItemEntry },
            OutputItems = new List<TradeSkillItemEntry>() { outputItemEntry },
        };
    }

    public ItemCraftingTemplate MakeCopperSwordCraftingTemplate()
    {
        var inputItem1Entry = new TradeSkillItemEntry() { TemplateId = ItemTemplateLookups.CopperIngot };
        inputItem1Entry.Variables.Amount(2);
        var inputItem2Entry = new TradeSkillItemEntry() { TemplateId = ItemTemplateLookups.OakLog };
        inputItem2Entry.Variables.Amount(1);

        var outputItemEntry = new TradeSkillItemEntry() { TemplateId = ItemTemplateLookups.CopperSword };
        outputItemEntry.Variables.Amount(1);
        
        return new ItemCraftingTemplate()
        {
            Id = ItemCraftingTemplateLookups.CopperSword,
            SkillType = FantasyCraftingTradeSkillTypes.Smithing,
            SkillDifficulty = 10,
            TimeToComplete = 2.0f,
            InputItems = new List<TradeSkillItemEntry>() { inputItem1Entry, inputItem2Entry },
            OutputItems = new List<TradeSkillItemEntry>() { outputItemEntry },
            Requirements = new []
            {
                new Requirement { RequirementType = FantasyRequirementTypes.TradeSkillRequirement, Association = new Association(FantasyCraftingTradeSkillTypes.Smithing, 5) }
            }
        };
    }
}
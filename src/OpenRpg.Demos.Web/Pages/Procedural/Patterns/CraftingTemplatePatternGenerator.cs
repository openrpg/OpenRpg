using OpenRpg.Core.Associations;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates;
using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Procedural.Patterns;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.Templates;
using OpenRpg.Items.TradeSkills;
using OpenRpg.Items.TradeSkills.Templates;
using OpenRpg.Items.TradeSkills.Extensions;

namespace OpenRpg.Demos.Web.Pages.Procedural.Patterns;

public class CraftingTemplatePatternGenerator : ITemplatePatternGenerator<ItemCraftingTemplate, ItemCraftingTemplatePatternGeneratorConfig>
{
    public ITemplateAccessor TemplateAccessor { get; }

    public CraftingTemplatePatternGenerator(ITemplateAccessor templateAccessor)
    {
        TemplateAccessor = templateAccessor;
    }

    public IReadOnlyCollection<ItemCraftingTemplate> Generate(ItemCraftingTemplatePatternGeneratorConfig config)
    {
        var itemCraftingTemplates = new List<ItemCraftingTemplate>();
        var id = config.StartingId;
        var patternIds = config.PatternIds;
        
        for (var i = 0; i < patternIds.Length; i++)
        {
            var patternId = patternIds[i];

            var relatedPatternItemTemplates = TemplateAccessor
                .GetAll<ItemTemplate>()
                .Where(x => x.Variables.PatternId == patternId)
                .ToArray();

            var inputTemplate = relatedPatternItemTemplates.First(x => x.Variables.PatternTypeId == config.InputPatternTypeId);
            var inputItemEntry = new TradeSkillItemEntry { TemplateId = inputTemplate.Id };
            inputItemEntry.Variables.Amount = config.AmountRequired;
            var inputItems = new List<TradeSkillItemEntry> { inputItemEntry };

            var outputTemplate = relatedPatternItemTemplates.First(x => x.Variables.PatternTypeId == config.OutputPatternTypeId);
            var outputItemEntry = new TradeSkillItemEntry { TemplateId = outputTemplate.Id };
            outputItemEntry.Variables.Amount = config.AmountOutput;
            var outputItems = new List<TradeSkillItemEntry> { outputItemEntry };
            
            var nameLocaleId = config.LocaleGenerator.GenerateNameLocaleId(patternId, config.TypeCode);
            var descriptionLocaleId = config.LocaleGenerator.GenerateDescriptionLocaleId(patternId, config.TypeCode);
            
            var template = new ItemCraftingTemplate();
            template.Id = id++;
            // TODO: Need to fix how this gets generated
            //template.NameLocaleId = nameLocaleId;
            //template.DescriptionLocaleId = descriptionLocaleId;
            template.InputItems = inputItems;
            template.OutputItems = outputItems;
            template.Variables.PatternId = patternId;
            template.Variables.PatternTypeId = config.PatternTypeId;
            template.Variables.SkillType = config.SkillType;
            
            var skillDifficulty = (int)Math.Round(config.SkillScore.Plot(i));
            if(skillDifficulty > 0)
            {
                template.Variables.Requirements = new List<Requirement>
                {
                    new()
                    {
                        RequirementType = FantasyRequirementTypes.TradeSkillRequirement,
                        Association = new Association(config.SkillType, skillDifficulty)
                    }
                };
            }
            
            itemCraftingTemplates.Add(template);
        }

        return itemCraftingTemplates;
    }
}
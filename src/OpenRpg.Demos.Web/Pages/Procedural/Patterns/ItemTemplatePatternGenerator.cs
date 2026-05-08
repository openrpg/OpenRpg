using OpenRpg.Entities.Extensions;
using OpenRpg.Entities.Procedural.Patterns;
using OpenRpg.Genres.Fantasy.Types;
using OpenRpg.Items.Templates;
using OpenRpg.Items.Extensions;

namespace OpenRpg.Demos.Web.Pages.Procedural.Patterns;

public class ItemTemplatePatternGenerator : ITemplatePatternGenerator<ItemTemplate, ItemTemplatePatternGeneratorConfig>
{
    public IReadOnlyCollection<ItemTemplate> Generate(ItemTemplatePatternGeneratorConfig config)
    {
        var itemTemplates = new List<ItemTemplate>();
        var id = config.StartingId;
        var patternIds = config.PatternIds;
        for (var i = 0; i < patternIds.Length; i++)
        {
            var patternId = patternIds[i];
            var nameLocaleId = config.LocaleGenerator.GenerateNameLocaleId(patternId, config.TypeCode);
            
            var template = new ItemTemplate
            {
                Id = id++,
                NameLocaleId = nameLocaleId,
                ItemType = config.ItemType
            };
            template.Variables.AssetCode = nameLocaleId.ToLower().Replace(" ", "-");
            template.Variables.QualityType = FantasyItemQualityTypes.CommonQuality;
            template.Variables.PatternTypeId = config.PatternTypeId;
            template.Variables.PatternId = patternId;

            if (config.Effects is not null)
            {
                var resultingEffects = config.Effects.GenerateEffectsFrom(i);
                template.Variables.Effects = resultingEffects;
            }
            
            itemTemplates.Add(template);
        }

        return itemTemplates;
    }
}
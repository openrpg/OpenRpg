using Microsoft.Extensions.Logging;
using OpenRpg.Editor.Core.Plugins;
using OpenRpg.Editor.Infrastructure.Services;

namespace OpenRpg.Editor.UI.Services;

public class TemplateOptionsResolver : ITemplateOptionsResolver
{
    private readonly GenreTypesService _genreTypes;
    private readonly ILogger<TemplateOptionsResolver> _logger;

    public TemplateOptionsResolver(GenreTypesService genreTypes, ILogger<TemplateOptionsResolver> logger)
    {
        _genreTypes = genreTypes;
        _logger = logger;
    }

    public OptionData[] GetOptionsForType(string typeSource, string templateTypeName = "")
    {
        var finalTypeSource = ResolveSkillType(typeSource, templateTypeName);

        var result = finalTypeSource switch
        {
            "itemTypes" => _genreTypes.ItemTypes,
            "itemQualityTypes" => _genreTypes.ItemQualityTypes,
            "modificationTypes" => _genreTypes.ModificationTypes,
            "effectTypes" => _genreTypes.EffectTypes,
            "genderTypes" => _genreTypes.GenderTypes,
            "requirementTypes" => _genreTypes.RequirementTypes,
            "objectiveTypes" => _genreTypes.ObjectiveTypes,
            "rewardTypes" => _genreTypes.RewardTypes,
            "gatheringSkillTypes" => _genreTypes.GatheringSkillTypes,
            "craftingSkillTypes" => _genreTypes.CraftingSkillTypes,
            "effectScalingType" => _genreTypes.EffectScalingType,
            "statTypes" => _genreTypes.StatTypes,
            "stateTypes" => _genreTypes.StateTypes,
            "targetTypes" => _genreTypes.TargetTypes,
            "damageTypes" => _genreTypes.DamageTypes,
            "itemSlotTypes" => _genreTypes.ItemSlotTypes,
            _ => ResolveUnknownTypeSource(finalTypeSource)
        };

        return result;
    }

    private OptionData[] ResolveUnknownTypeSource(string typeSource)
    {
        _logger.LogWarning("Unknown type source '{TypeSource}' requested - returning empty options", typeSource);
        return System.Array.Empty<OptionData>();
    }

    private static string ResolveSkillType(string typeSource, string templateTypeName)
    {
        if (string.IsNullOrEmpty(templateTypeName) || !typeSource.Contains("SkillType"))
            return typeSource;

        if (templateTypeName == "ItemCraftingTemplate") return "craftingSkillTypes";
        if (templateTypeName == "ItemGatheringTemplate") return "gatheringSkillTypes";

        return typeSource;
    }
}

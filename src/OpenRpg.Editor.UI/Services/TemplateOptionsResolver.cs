using OpenRpg.Editor.Core.Plugins;
using OpenRpg.Editor.Infrastructure.Services;

namespace OpenRpg.Editor.UI.Services;

public class TemplateOptionsResolver : ITemplateOptionsResolver
{
    private readonly GenreTypesService _genreTypes;

    public TemplateOptionsResolver(GenreTypesService genreTypes)
    {
        _genreTypes = genreTypes;
    }

    public OptionData[] GetOptionsForType(string typeSource, string templateTypeName = "")
    {
        var finalTypeSource = ResolveSkillType(typeSource, templateTypeName);

        return finalTypeSource switch
        {
            "itemTypes" => _genreTypes.ItemTypes,
            "itemQualityTypes" => _genreTypes.ItemQualityTypes,
            "modificationTypes" => _genreTypes.ModificationTypes,
            "effectTypes" => _genreTypes.EffectTypes,
            "requirementTypes" => _genreTypes.RequirementTypes,
            "objectiveTypes" => _genreTypes.ObjectiveTypes,
            "rewardTypes" => _genreTypes.RewardTypes,
            "gatheringSkillTypes" => _genreTypes.GatheringSkillTypes,
            "craftingSkillTypes" => _genreTypes.CraftingSkillTypes,
            "effectScalingType" => _genreTypes.EffectScalingType,
            "statTypes" => _genreTypes.StatTypes,
            "stateTypes" => _genreTypes.StateTypes,
            _ => System.Array.Empty<OptionData>()
        };
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

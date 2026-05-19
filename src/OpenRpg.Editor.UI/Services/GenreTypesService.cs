using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using OpenRpg.Editor.Core.Plugins;
using OpenRpg.Editor.Infrastructure.Plugins;

namespace OpenRpg.Editor.UI.Services;

public class GenreTypesService
{
    private readonly GenreService _genreService;
    private IGenreTypesProvider _overrideProvider;

    public GenreTypesService(GenreService genreService)
    {
        _genreService = genreService;
    }

    public void SetTypesProvider(IGenreTypesProvider typesProvider)
    {
        _overrideProvider = typesProvider;
    }

    public IGenreTypesProvider TypesProvider => 
        _overrideProvider ?? _genreService.GetCombinedTypesProvider();

    public OptionData[] ItemTypes => TypesProvider.ItemTypes;
    public OptionData[] ItemQualityTypes => TypesProvider.ItemQualityTypes;
    public OptionData[] RequirementTypes => TypesProvider.RequirementTypes;
    public OptionData[] EffectTypes => TypesProvider.EffectTypes;
    public OptionData[] GenderTypes => TypesProvider.GenderTypes;
    public OptionData[] RewardTypes => TypesProvider.RewardTypes;
    public OptionData[] ModificationTypes => TypesProvider.ModificationTypes;
    public OptionData[] ObjectiveTypes => TypesProvider.ObjectiveTypes;
    public OptionData[] EffectScalingType => TypesProvider.EffectScalingType;
    public OptionData[] StatTypes => TypesProvider.StatTypes;
    public OptionData[] StateTypes => TypesProvider.StateTypes;
    public OptionData[] CraftingSkillTypes => TypesProvider.CraftingSkillTypes;
    public OptionData[] GatheringSkillTypes => TypesProvider.GatheringSkillTypes;
    public OptionData[] TargetTypes => TypesProvider.TargetTypes;
    public OptionData[] DamageTypes => TypesProvider.DamageTypes;
    public OptionData[] ItemSlotTypes => TypesProvider.ItemSlotTypes;
}

public class EmptyTypesProvider : IGenreTypesProvider
{
    public string PluginId => "empty";
    public OptionData[] ItemTypes => Array.Empty<OptionData>();
    public OptionData[] ItemQualityTypes => Array.Empty<OptionData>();
    public OptionData[] RequirementTypes => Array.Empty<OptionData>();
    public OptionData[] EffectTypes => Array.Empty<OptionData>();
    public OptionData[] GenderTypes => Array.Empty<OptionData>();
    public OptionData[] RewardTypes => Array.Empty<OptionData>();
    public OptionData[] ModificationTypes => Array.Empty<OptionData>();
    public OptionData[] ObjectiveTypes => Array.Empty<OptionData>();
    public OptionData[] EffectScalingType => Array.Empty<OptionData>();
    public OptionData[] StatTypes => Array.Empty<OptionData>();
    public OptionData[] StateTypes => Array.Empty<OptionData>();
    public OptionData[] CraftingSkillTypes => Array.Empty<OptionData>();
    public OptionData[] GatheringSkillTypes => Array.Empty<OptionData>();
    public OptionData[] TargetTypes => Array.Empty<OptionData>();
    public OptionData[] DamageTypes => Array.Empty<OptionData>();
    public OptionData[] ItemSlotTypes => Array.Empty<OptionData>();
}

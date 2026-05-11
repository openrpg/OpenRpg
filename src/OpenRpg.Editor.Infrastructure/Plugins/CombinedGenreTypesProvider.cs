using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenRpg.Editor.Core.Plugins;

namespace OpenRpg.Editor.Infrastructure.Plugins;

public class CombinedGenreTypesProvider : IGenreTypesProvider
{
    public string PluginId => "combined";

    public OptionData[] ItemTypes => _providers.SelectMany(p => p.ItemTypes).ToArray();
    public OptionData[] ItemQualityTypes => _providers.SelectMany(p => p.ItemQualityTypes).ToArray();
    public OptionData[] RequirementTypes => _providers.SelectMany(p => p.RequirementTypes).ToArray();
    public OptionData[] EffectTypes => _providers.SelectMany(p => p.EffectTypes).ToArray();
    public OptionData[] RewardTypes => _providers.SelectMany(p => p.RewardTypes).ToArray();
    public OptionData[] ModificationTypes => _providers.SelectMany(p => p.ModificationTypes).ToArray();
    public OptionData[] ObjectiveTypes => _providers.SelectMany(p => p.ObjectiveTypes).ToArray();
    public OptionData[] EffectScalingType => _providers.SelectMany(p => p.EffectScalingType).ToArray();
    public OptionData[] StatTypes => _providers.SelectMany(p => p.StatTypes).ToArray();
    public OptionData[] StateTypes => _providers.SelectMany(p => p.StateTypes).ToArray();
    public OptionData[] CraftingSkillTypes => _providers.SelectMany(p => p.CraftingSkillTypes).ToArray();
    public OptionData[] GatheringSkillTypes => _providers.SelectMany(p => p.GatheringSkillTypes).ToArray();

    private readonly List<IGenreTypesProvider> _providers;

    public CombinedGenreTypesProvider(IEnumerable<IGenreTypesProvider> providers)
    {
        _providers = providers.ToList();
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenRpg.Editor.Core.Plugins;

namespace OpenRpg.Editor.Infrastructure.Plugins;

public class CombinedGenreTypesProvider : IGenreTypesProvider
{
    public string PluginId => "combined";

    public OptionData[] ItemTypes => _providers.SelectMany(p => p.ItemTypes).GroupBy(o => o.Id).Select(g => g.First()).ToArray();
    public OptionData[] ItemQualityTypes => _providers.SelectMany(p => p.ItemQualityTypes).GroupBy(o => o.Id).Select(g => g.First()).ToArray();
    public OptionData[] RequirementTypes => _providers.SelectMany(p => p.RequirementTypes).GroupBy(o => o.Id).Select(g => g.First()).ToArray();
    public OptionData[] EffectTypes => _providers.SelectMany(p => p.EffectTypes).GroupBy(o => o.Id).Select(g => g.First()).ToArray();
    public OptionData[] GenderTypes => _providers.SelectMany(p => p.GenderTypes).GroupBy(o => o.Id).Select(g => g.First()).ToArray();
    public OptionData[] RewardTypes => _providers.SelectMany(p => p.RewardTypes).GroupBy(o => o.Id).Select(g => g.First()).ToArray();
    public OptionData[] ModificationTypes => _providers.SelectMany(p => p.ModificationTypes).GroupBy(o => o.Id).Select(g => g.First()).ToArray();
    public OptionData[] ObjectiveTypes => _providers.SelectMany(p => p.ObjectiveTypes).GroupBy(o => o.Id).Select(g => g.First()).ToArray();
    public OptionData[] EffectScalingType => _providers.SelectMany(p => p.EffectScalingType).GroupBy(o => o.Id).Select(g => g.First()).ToArray();
    public OptionData[] StatTypes => _providers.SelectMany(p => p.StatTypes).GroupBy(o => o.Id).Select(g => g.First()).ToArray();
    public OptionData[] StateTypes => _providers.SelectMany(p => p.StateTypes).GroupBy(o => o.Id).Select(g => g.First()).ToArray();
    public OptionData[] CraftingSkillTypes => _providers.SelectMany(p => p.CraftingSkillTypes).GroupBy(o => o.Id).Select(g => g.First()).ToArray();
    public OptionData[] GatheringSkillTypes => _providers.SelectMany(p => p.GatheringSkillTypes).GroupBy(o => o.Id).Select(g => g.First()).ToArray();

    private readonly List<IGenreTypesProvider> _providers;

    public CombinedGenreTypesProvider(IEnumerable<IGenreTypesProvider> providers)
    {
        _providers = providers.ToList();
    }
}
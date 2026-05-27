using System;
using OpenRpg.Editor.Core.Plugins;

namespace OpenRpg.Editor.Infrastructure.Plugins;

public class EmptyGenreTypesProvider : IGenreTypesProvider
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
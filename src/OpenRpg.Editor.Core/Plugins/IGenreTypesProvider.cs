namespace OpenRpg.Editor.Core.Plugins;

public interface IGenreTypesProvider
{
    string PluginId { get; }
    
    OptionData[] ItemTypes { get; }
    OptionData[] ItemQualityTypes { get; }
    OptionData[] RequirementTypes { get; }
    OptionData[] EffectTypes { get; }
    OptionData[] RewardTypes { get; }
    OptionData[] ModificationTypes { get; }
    OptionData[] GenderTypes { get; }
    OptionData[] ObjectiveTypes { get; }
    OptionData[] EffectScalingType { get; }
    OptionData[] StatTypes { get; }
    OptionData[] StateTypes { get; }
    OptionData[] CraftingSkillTypes { get; }
    OptionData[] GatheringSkillTypes { get; }
    OptionData[] TargetTypes { get; }
    OptionData[] DamageTypes { get; }
    OptionData[] ItemSlotTypes { get; }
}
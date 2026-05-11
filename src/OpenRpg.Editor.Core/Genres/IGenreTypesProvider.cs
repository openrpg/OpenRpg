namespace OpenRpg.Editor.Core.Genres;

public interface IGenreTypesProvider
{
    string GenreId { get; }
    string GenreName { get; }

    OptionData[] ItemTypes { get; }
    OptionData[] ItemQualityTypes { get; }
    OptionData[] RequirementTypes { get; }
    OptionData[] EffectTypes { get; }
    OptionData[] RewardTypes { get; }
    OptionData[] ModificationTypes { get; }
    OptionData[] ObjectiveTypes { get; }
    OptionData[] EffectScalingType { get; }
    OptionData[] StatTypes { get; }
    OptionData[] StateTypes { get; }
    OptionData[] CraftingSkillTypes { get; }
    OptionData[] GatheringSkillTypes { get; }
}

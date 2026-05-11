using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Editor.Core.Genres;
using OpenRpg.Entities.Types;
using OpenRpg.Genres.Requirements;
using OpenRpg.Genres.Types;
using OpenRpg.Quests.Types;

namespace OpenRpg.Editor.Infrastructure.Plugins;

public class DefaultGenreTypesProvider : IGenreTypesProvider
{
    public string GenreId => "default";
    public string GenreName => "Default Types";

    public OptionData[] ItemTypes => new OptionData[]
    {
        new(0, "Unknown"),
        new(GenresItemTypes.QuestItem, "Quest Item"),
        new(GenresItemTypes.GenericWeapon, "Weapon"),
        new(GenresItemTypes.GenericItem, "Generic Item")
    };

    public OptionData[] ItemQualityTypes => new OptionData[]
    {
        new(0, "Unknown"),
        new(GenresItemQualityTypes.JunkQuality, "Junk"),
        new(GenresItemQualityTypes.CommonQuality, "Common"),
        new(GenresItemQualityTypes.RareQuality, "Rare")
    };

    public OptionData[] RequirementTypes => new OptionData[]
    {
        new(GenreRequirementTypes.MaxHealthRequirement, "Max Health"),
        new(GenreRequirementTypes.MaxStaminaRequirement, "Max Stamina"),
        new(GenreRequirementTypes.MovementSpeedRequirement, "Movement Speed")
    };

    public OptionData[] EffectTypes => new OptionData[]
    {
        new(0, "Unknown"),
        new(GenreEffectTypes.DamageBonusAmount, "Damage Bonus"),
        new(GenreEffectTypes.DamageBonusPercentage, "Damage Bonus %"),
        new(GenreEffectTypes.CriticalRateBonusAmount, "Critical Rate"),
        new(GenreEffectTypes.CriticalDamageBonusAmount, "Critical Damage"),
        new(GenreEffectTypes.DefenseBonusAmount, "Defense Bonus"),
        new(GenreEffectTypes.HealthBonusAmount, "Max Health Bonus"),
        new(GenreEffectTypes.HealthRestoreAmount, "Heal Amount"),
        new(GenreEffectTypes.StaminaBonusAmount, "Max Stamina Bonus"),
        new(GenreEffectTypes.StaminaRestoreAmount, "Stamina Restore"),
        new(GenreEffectTypes.MovementSpeedBonusAmount, "Movement Speed Bonus"),
        new(GenreEffectTypes.ExperienceRestoreAmount, "Experience")
    };

    public OptionData[] RewardTypes => new OptionData[]
    {
        new(0, "Unknown"),
        new(GenreRewardTypes.ItemReward, "Item Reward"),
        new(GenreRewardTypes.QuestReward, "Quest Reward"),
        new(GenreRewardTypes.TriggerReward, "Trigger"),
        new(GenreRewardTypes.CurrencyReward, "Currency")
    };

    public OptionData[] ModificationTypes => new OptionData[]
    {
        new(0, "Unknown")
    };

    public OptionData[] ObjectiveTypes => new OptionData[]
    {
        new(Quests.Types.ObjectiveTypes.UnknownObjective, "Unknown"),
        new(Quests.Types.ObjectiveTypes.TriggerObjective, "Trigger"),
        new(Quests.Types.ObjectiveTypes.ItemObjective, "Collect Item"),
        new(Quests.Types.ObjectiveTypes.LevelObjective, "Reach Level"),
        new(Quests.Types.ObjectiveTypes.ClassObjective, "Reach Class"),
        new(GenresObjectiveTypes.CurrencyObjective, "Currency"),
        new(GenresObjectiveTypes.EnemyDefeatedObjective, "Defeat Enemy"),
        new(GenresObjectiveTypes.EnemySightedObjective, "See Enemy")
    };

    public OptionData[] EffectScalingType => new OptionData[]
    {
        new(0, "None"),
        new(1, "Linear"),
        new(2, "Percentage")
    };

    public OptionData[] StatTypes => new OptionData[]
    {
        new(0, "Unknown"),
        new(1, "Strength"),
        new(2, "Dexterity"),
        new(3, "Intelligence"),
        new(4, "Wisdom"),
        new(5, "Endurance"),
        new(6, "Charisma"),
        new(GenreEntityStatsVariableTypes.MaxHealth, "Max Health"),
        new(GenreEntityStatsVariableTypes.HealthRegen, "Health Regen"),
        new(GenreEntityStatsVariableTypes.MaxStamina, "Max Stamina"),
        new(GenreEntityStatsVariableTypes.StaminaRegen, "Stamina Regen"),
        new(GenreEntityStatsVariableTypes.MovementSpeed, "Movement Speed"),
        new(GenreEntityStatsVariableTypes.Damage, "Damage"),
        new(GenreEntityStatsVariableTypes.CriticalDamageChance, "Critical Chance"),
        new(GenreEntityStatsVariableTypes.CriticalDamageMultiplier, "Critical Damage"),
        new(GenreEntityStatsVariableTypes.Defense, "Defense")
    };

    public OptionData[] StateTypes => new OptionData[]
    {
        new(0, "Unknown"),
        new(GenreEntityStateVariableTypes.Health, "Health"),
        new(GenreEntityStateVariableTypes.Stamina, "Stamina")
    };

    public OptionData[] CraftingSkillTypes => new OptionData[]
    {
        new(0, "Unknown")
    };

    public OptionData[] GatheringSkillTypes => new OptionData[]
    {
        new(0, "Unknown")
    };
}

public class DefaultGenreRuntimeServices : IGenreRuntimeServices
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<ICharacterRequirementChecker, DefaultCharacterRequirementChecker>();
    }
}

public class DefaultGenrePlugin : IGenrePlugin
{
    public string Id => "default";
    public string Name => "Default Types";
    public string Version => "1.0.0";
    public string Description => "Built-in default types from OpenRpg.Genres library";

    public IGenreTypesProvider TypesProvider { get; private set; }
    public IGenreRuntimeServices RuntimeServices { get; private set; }

    public DefaultGenrePlugin()
    {
        TypesProvider = new DefaultGenreTypesProvider();
        RuntimeServices = new DefaultGenreRuntimeServices();
    }

    public void Initialize(IServiceCollection services)
    {
        RuntimeServices.RegisterServices(services);
    }
}
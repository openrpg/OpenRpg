using System;
using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Combat.Abilities;
using OpenRpg.Combat.Attacks;
using OpenRpg.Combat.Processors.Attacks.Entity;
using OpenRpg.Combat.Types;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Entities.Types;
using OpenRpg.Items.Types;
using OpenRpg.Projects.Json.Convertors;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Utils;
using OpenRpg.Data;
using OpenRpg.Data.InMemory;
using OpenRpg.Demos.Battler.Code.Scenes;
using OpenRpg.Demos.Battler.Code.Scenes.Battle;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Models;
using OpenRpg.Demos.Battler.Code.Scenes.CharacterMenu;
using OpenRpg.Demos.Battler.Code.Scenes.Battle.Providers;
using OpenRpg.Demos.Battler.Code.Services;
using OpenRpg.Demos.Battler.Code.Services.Game;
using OpenRpg.Demos.Battler.Code.Types;
using OpenRpg.Entities.Entity.Populators.State;
using OpenRpg.Entities.Entity.Populators.Stats;
using OpenRpg.Genres.Effects;
using OpenRpg.Genres.Fantasy.Combat;
using OpenRpg.Genres.Fantasy.Equippables.Validators;
using OpenRpg.Genres.Fantasy.State.Populators;
using OpenRpg.Genres.Fantasy.Stats.Populators;
using OpenRpg.Genres.Populators.Entity;
using OpenRpg.Genres.Requirements;
using OpenRpg.Items.Equippables.Slots;
using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Projects.Json.Loaders;
using OpenRpg.Projects.Json.Loaders.Locales;
using OpenRpg.Projects.Json.Loaders.Templates;
using OpenRpg.Projects.Loaders;
using OpenRpg.Projects.Loaders.Locales;
using OpenRpg.Projects.Loaders.Templates;
using OpenRpg.Projects.Loaders.Projects;
using OpenRpg.Projects.Services;

namespace OpenRpg.Demos.Battler.Code.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection WithOpenRpgFramework(this IServiceCollection services)
    {
        services.AddSingleton<IRandomizer>(x => new DefaultRandomizer(new Random()));
            
        services.AddSingleton<IEntityAttackGenerator, FantasyAttackGenerator>();
        services.AddSingleton<IEntityAttackProcessor, DefaultAttackProcessor>();

        services.AddSingleton<IEntityStatPopulator, FantasyStatsPopulator>();
        services.AddSingleton<IEntityStatePopulator, FantasyStatePopulator>();
        services.AddSingleton<ICharacterEffectProcessor, CharacterEffectProcessor>();
        services.AddSingleton<ICharacterPopulator, CharacterPopulator>();
        services.AddSingleton<ICharacterRequirementChecker, DefaultCharacterRequirementChecker>();
        services.AddSingleton<IEquipmentSlotValidator, FantasyCharacterEquipmentSlotValidator>();

        services.AddSingleton<ITemplateAccessor, TemplateAccessor>();
        services.AddSingleton<Builders.GameCharacterBuilder>();
        
        return services;
    }
    
    public static IServiceCollection WithOpenRpgProject(this IServiceCollection services)
    {
        VariablesConverter.RegisterKey(CoreTemplateVariableTypes.Effects, typeof(IEffect));
        VariablesConverter.RegisterKey(CoreTemplateVariableTypes.Requirements, typeof(Requirement));
        VariablesConverter.RegisterKey(CombatAbilityTemplateVariableTypes.Damage, typeof(Damage));
        VariablesConverter.RegisterKey(CombatTemplateVariableTypes.Abilities, typeof(AbilityData));
        VariablesConverter.RegisterKey(LootTableEntryVariableTypes.Requirements, typeof(Requirement));
        VariablesConverter.RegisterKey(DemoEntityVariableTypes.LootTable, typeof(SimpleLootEntry));

        services.AddSingleton<IFileService, DefaultFileService>();
        services.AddSingleton<IProjectLoader, JsonProjectLoader>();
        services.AddSingleton<ITemplateLoader, JsonTemplateLoader>();
        services.AddSingleton<ILocaleLoader, JsonLocaleLoader>();
        services.AddSingleton<ITemplateDatastorePopulator, JsonTemplateDatastorePopulator>();
        services.AddSingleton<ILocaleDatastorePopulator, JsonLocaleDatastorePopulator>();
        services.AddSingleton<IDataSource, InMemoryDataSource>();
        services.AddSingleton<ILocaleDataSource, InMemoryLocaleDataSource>();
        services.AddSingleton<IDataLoader, FileDataLoader>();

        return services;
    }

    public static IServiceCollection WithMonoGameServices(this IServiceCollection services)
    {
        services.AddSingleton<IGameServices, GameServices>();

        return services;
    }

    public static IServiceCollection WithSceneServices(this IServiceCollection services)
    {
        services.AddSingleton<ISceneManager, SceneManager>();
        services.AddSingleton<IPersistentGameState, PersistentGameState>();
        services.AddSingleton<IPartyProvider, PartyProvider>();
        services.AddSingleton<IEnemyFormationProvider, EnemyFormationProvider>();
        services.AddSingleton<ILootService, LootService>();
        services.AddTransient<BattleScene>();
        services.AddTransient<CharacterMenuScene>();

        return services;
    }
}
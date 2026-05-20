using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Data;
using OpenRpg.Data.InMemory;
using OpenRpg.Demos.Battler.Code.Scenes;
using OpenRpg.Demos.Battler.Code.Scenes.BattleScene;
using OpenRpg.Demos.Battler.Code.Services;
using OpenRpg.Demos.Battler.Code.Services.Game;
using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Projects.Json.Loaders;
using OpenRpg.Projects.Json.Loaders.Locales;
using OpenRpg.Projects.Json.Loaders.Templates;
using OpenRpg.Projects.Loaders;
using OpenRpg.Projects.Loaders.Locales;
using OpenRpg.Projects.Loaders.Projects;
using OpenRpg.Projects.Loaders.Templates;
using OpenRpg.Projects.Services;

namespace OpenRpg.Demos.Battler.Code.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection WithOpenRpgProject(this IServiceCollection services)
    {
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
        services.AddSingleton<IPartyProvider, PartyProvider>();
        services.AddSingleton<IEnemyFormationProvider, EnemyFormationProvider>();
        services.AddTransient<BattleScene>();

        return services;
    }
}
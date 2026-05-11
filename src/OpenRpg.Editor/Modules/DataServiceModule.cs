using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Data;
using OpenRpg.Editor.Core.Models;
using OpenRpg.Editor.Core.Services.Events.Broker;
using OpenRpg.Editor.Core.Services.Events.Bus;
using OpenRpg.Editor.Core.Services.FileSystem;
using OpenRpg.Editor.Core.Services.Modal;
using OpenRpg.Editor.Core.Services.Notifications;
using OpenRpg.Editor.Core.Services.Threading;
using OpenRpg.Editor.Infrastructure.Data;
using OpenRpg.Editor.Infrastructure.Persistence;
using OpenRpg.Editor.Infrastructure.Persistence.Migrations;
using OpenRpg.Editor.Infrastructure.Plugins;
using OpenRpg.Editor.Infrastructure.Services;
using OpenRpg.Editor.Services.FileSystem;
using OpenRpg.Editor.UI.Services;
using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Localization.Data.Repositories;
using OpenRpg.Projects.Json.Loaders;
using OpenRpg.Projects.Json.Loaders.Locales;
using OpenRpg.Projects.Json.Loaders.Templates;
using OpenRpg.Projects.Loaders;
using OpenRpg.Projects.Loaders.Locales;
using OpenRpg.Projects.Loaders.Projects;
using OpenRpg.Projects.Loaders.Templates;
using OpenRpg.Projects.Services;
using Persistity.Core.Serialization;
using Persistity.Flow.Builders;
using Persistity.Serializers.Json;

namespace OpenRpg.Editor.Modules
{
    public class DataServiceModule
    {
        public static void Setup(IServiceCollection services)
        {
            services.AddSingleton<ISerializer, JsonSerializer>();
            services.AddSingleton<IDeserializer, JsonDeserializer>();
            services.AddSingleton<ICloner, Cloner>();
            services.AddSingleton<PipelineBuilder>();
            services.AddTransient<IModalService, ModalService>();
            services.AddTransient<INotifier, Notifier>();
            services.AddSingleton<IFileBrowser, PhotinoNativeFileBrowser>();

            services.AddSingleton<IThreadHandler, ThreadHandler>();
            services.AddSingleton<IMessageBroker, MessageBroker>();
            services.AddSingleton<IEventBus, EventBus>();
            
            services.AddSingleton<EditorState>();
            services.AddSingleton<CreateProjectExecutor>();
            services.AddSingleton<SaveProjectExecutor>();
            services.AddSingleton<EditorDataLoader>();
            
            services.AddSingleton<EditorDatasource>();
            services.AddSingleton<IDataSource>(x => x.GetService<EditorDatasource>());
            services.AddSingleton<IRepository, Repository>();
            
            services.AddSingleton<IProjectMigration, ProjectMigration_1_0_0>();
            services.AddSingleton<IFileService, DefaultFileService>();
            services.AddSingleton<IProjectLoader, JsonProjectLoader>();
            services.AddSingleton<ITemplateLoader, JsonTemplateLoader>();
            services.AddSingleton<ITemplateDatastorePopulator, JsonTemplateDatastorePopulator>();
            services.AddSingleton<ILocaleLoader, JsonLocaleLoader>();
            services.AddSingleton<ILocaleDatastorePopulator, JsonLocaleDatastorePopulator>();

            services.AddSingleton<EditorLocaleDatasource>();
            services.AddSingleton<ILocaleDataSource>(x => x.GetService<EditorLocaleDatasource>());
            services.AddSingleton<ILocaleRepository>(x => new LocaleRepository(x.GetService<EditorLocaleDatasource>(), "en-gb"));
            
            services.AddSingleton<GenrePluginLoader>();
            services.AddSingleton<GenreService>();
            services.AddSingleton<GenreTypesService>();
        }
    }
}
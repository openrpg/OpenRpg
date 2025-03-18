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
using OpenRpg.Editor.Infrastructure.Services;
using OpenRpg.Editor.Services.FileSystem;
using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Localization.Data.Repositories;
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
            services.AddSingleton<LoadProjectExecutor>();
            
            services.AddSingleton<EditorDatasource>();
            services.AddSingleton<IDataSource>(x => x.GetService<EditorDatasource>());
            services.AddSingleton<IRepository, Repository>();
            
            services.AddSingleton<IProjectMigration, ProjectMigration_1_0_0>();

            services.AddSingleton<EditorLocaleDatasource>();
            services.AddSingleton<ILocaleDataSource>(x => x.GetService<EditorLocaleDatasource>());
            services.AddSingleton<ILocaleRepository>(x => new LocaleRepository(x.GetService<EditorLocaleDatasource>(), "en-gb"));
        }
    }
}
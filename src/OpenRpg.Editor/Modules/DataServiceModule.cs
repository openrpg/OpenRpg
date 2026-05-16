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
using OpenRpg.Editor.Infrastructure.Persistence.Loaders;
using OpenRpg.Editor.Infrastructure.Persistence.Migrations;
using OpenRpg.Editor.Infrastructure.Plugins;
using OpenRpg.Editor.Infrastructure.Services;
using OpenRpg.Editor.Services.FileSystem;
using OpenRpg.Editor.UI.Components.Editors.Common;
using OpenRpg.Editor.UI.Components.Editors.List;
using OpenRpg.Editor.UI.Services;
using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Localization.Data.Repositories;
using OpenRpg.Projects.Json.Loaders;
using OpenRpg.Projects.Json.Loaders.Locales;
using OpenRpg.Projects.Json.Loaders.Templates;
using OpenRpg.Projects.Loaders.Locales;
using OpenRpg.Projects.Loaders.Projects;
using OpenRpg.Projects.Loaders.Templates;
using OpenRpg.Projects.Services;

namespace OpenRpg.Editor.Modules
{
    public class DataServiceModule
    {
        public static void Setup(IServiceCollection services)
        {
            services.AddSingleton<ICloner, Cloner>();
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
            services.AddSingleton<ILocaleLoader, JsonLocaleLoader>();
            services.AddSingleton<ILocaleDatastorePopulator, JsonLocaleDatastorePopulator>();

            services.AddSingleton<EditorLocaleDatasource>();
            services.AddSingleton<ILocaleDataSource>(x => x.GetService<EditorLocaleDatasource>());
            services.AddSingleton<ILocaleRepository>(x => new LocaleRepository(x.GetService<EditorLocaleDatasource>(), "en-gb"));
            
            services.AddSingleton<ManifestPluginLoader>();
            services.AddSingleton<GenreService>();
            services.AddSingleton<GenreTypesService>();
            services.AddSingleton<ITemplateDatastorePopulator, DynamicTemplateDatastorePopulator>();

            services.AddSingleton<IComponentTypeRegistry>(sp =>
            {
                var registry = new ComponentTypeRegistry();
                registry.Register(nameof(ItemTemplateDetailsEditor), typeof(ItemTemplateDetailsEditor));
                registry.Register(nameof(EntityTemplateDetailsEditor), typeof(EntityTemplateDetailsEditor));
                registry.Register(nameof(TradeSkillDetailsEditor), typeof(TradeSkillDetailsEditor));
                registry.Register(nameof(ObjectivesEditor), typeof(ObjectivesEditor));
                registry.Register(nameof(RewardsEditor), typeof(RewardsEditor));
                registry.Register(nameof(ModificationAllowancesEditor), typeof(ModificationAllowancesEditor));
                registry.Register(nameof(TradeSkillItemEntriesEditor), typeof(TradeSkillItemEntriesEditor));
                registry.Register(nameof(AbilitiesEditor), typeof(AbilitiesEditor));
                registry.Register(nameof(LootTableEditor), typeof(LootTableEditor));
                registry.Register(nameof(EquipmentEditor), typeof(EquipmentEditor));
                registry.Register(nameof(EffectsEditor), typeof(EffectsEditor));
                registry.Register(nameof(RequirementsEditor), typeof(RequirementsEditor));
                return registry;
            });
            services.AddSingleton<IEditorPropertyResolver, EditorPropertyResolver>();
            services.AddSingleton<ITemplateOptionsResolver, TemplateOptionsResolver>();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Data;
using OpenRpg.Editor.Core.Models;
using OpenRpg.Editor.Infrastructure.Data;
using OpenRpg.Editor.Infrastructure.Persistence.Migrations;
using OpenRpg.Editor.Infrastructure.Plugins;
using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Projects.Loaders;
using OpenRpg.Projects.Loaders.Locales;
using OpenRpg.Projects.Loaders.Projects;
using OpenRpg.Projects.Loaders.Templates;
using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Infrastructure.Persistence;

public class EditorDataLoader : FileDataLoader
{
    public EditorState EditorState { get; }
    public IEnumerable<IProjectMigration> ProjectMigrations { get; }
    public GenreService GenreService { get; }

    public EditorDataLoader(IDataSource datasource, ILocaleDataSource localeDatasource, IProjectLoader projectLoader, ITemplateDatastorePopulator templateDatastorePopulator, ILocaleDatastorePopulator localeDatastorePopulator, EditorState editorState, IEnumerable<IProjectMigration> projectMigrations, GenreService genreService) : base(datasource, localeDatasource, projectLoader, templateDatastorePopulator, localeDatastorePopulator)
    {
        EditorState = editorState;
        ProjectMigrations = projectMigrations;
        GenreService = genreService;

        ProjectLoaded += OnProjectLoaded;
    }

    public void OnProjectLoaded(object sender, ProjectContext projectContext)
    {
        EditorState.ProjectContext = projectContext;
        
        var project = projectContext.Project;
        if (project.Plugins.Count <= 0) { return; }
        
        var pluginIds = project.Plugins.Select(p => p.Id).ToList();
        GenreService.SetEnabledPlugins(pluginIds);

        if (Datasource is not EditorDatasource editorDs) { return; } 
        
        var templateTypeRegistry = GenreService.GetTemplateTypeRegistry();
        foreach (var templateType in templateTypeRegistry.GetTemplateTypes())
        {
            var classType = templateTypeRegistry.GetTemplateClassType(templateType);
            if (classType != null && !editorDs.Database.ContainsKey(classType))
            {
                editorDs.Database.Add(classType, new Dictionary<object, object>());
            }
        }
    }
}
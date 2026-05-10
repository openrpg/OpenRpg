using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OpenRpg.Data;
using OpenRpg.Editor.Core.Models;
using OpenRpg.Editor.Infrastructure.Persistence.Migrations;
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

    public EditorDataLoader(IDataSource datasource, ILocaleDataSource localeDatasource, IProjectLoader projectLoader, ITemplateDatastorePopulator templateDatastorePopulator, ILocaleDatastorePopulator localeDatastorePopulator, EditorState editorState, IEnumerable<IProjectMigration> projectMigrations) : base(datasource, localeDatasource, projectLoader, templateDatastorePopulator, localeDatastorePopulator)
    {
        EditorState = editorState;
        ProjectMigrations = projectMigrations;
    }

    public override async Task<Project> Load(string projectFile)
    {
        var project = await base.Load(projectFile);
        
        var projectPath = Path.GetDirectoryName(projectFile);
        EditorState.CurrentProject = new LoadedProject() { Project = project, ProjectPath = projectPath };

        return project;
    }
}
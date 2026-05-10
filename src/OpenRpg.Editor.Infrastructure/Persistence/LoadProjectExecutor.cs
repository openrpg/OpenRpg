using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OpenRpg.Editor.Core.Models;
using OpenRpg.Editor.Infrastructure.Data;
using OpenRpg.Editor.Infrastructure.Persistence.Migrations;
using OpenRpg.Projects.Loaders;
using OpenRpg.Projects.Loaders.Locales;
using OpenRpg.Projects.Loaders.Templates;

namespace OpenRpg.Editor.Infrastructure.Persistence;

public class LoadProjectExecutor
{
    public EditorState EditorState { get; }
    public EditorDatasource EditorDatasource { get; }
    public EditorLocaleDatasource EditorLocaleDatasource { get; }
    public IEnumerable<IProjectMigration> ProjectMigrations { get; }
    public IProjectLoader ProjectLoader { get; }
    public ITemplateDatastorePopulator TemplateDatastorePopulator { get; }
    public ILocaleDatastorePopulator LocaleDatastorePopulator { get; }
    
    public LoadProjectExecutor(EditorState editorState, EditorDatasource editorDatasource, EditorLocaleDatasource editorLocaleDatasource, IEnumerable<IProjectMigration> projectMigrations, IProjectLoader projectLoader, ITemplateDatastorePopulator templateDatastorePopulator, ILocaleDatastorePopulator localeDatastorePopulator)
    {
        EditorState = editorState;
        EditorDatasource = editorDatasource;
        EditorLocaleDatasource = editorLocaleDatasource;
        ProjectMigrations = projectMigrations;
        ProjectLoader = projectLoader;
        TemplateDatastorePopulator = templateDatastorePopulator;
        LocaleDatastorePopulator = localeDatastorePopulator;
    }
    
    public async Task Execute(string projectFile)
    {
        var project = await ProjectLoader.LoadProject(projectFile);
        var projectPath = Path.GetDirectoryName(projectFile);
        EditorState.CurrentProject = new LoadedProject() { Project = project, ProjectPath = projectPath };
        await TemplateDatastorePopulator.PopulateDatastore(project, projectPath, EditorDatasource);
        await LocaleDatastorePopulator.PopulateDatastore(project, projectPath, EditorLocaleDatasource);
    }
}
using OpenRpg.Data;
using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Projects.Loaders.Locales;
using OpenRpg.Projects.Loaders.Projects;
using OpenRpg.Projects.Loaders.Templates;
using OpenRpg.Projects.Models;

namespace OpenRpg.Projects.Loaders;

public class FileDataLoader : IDataLoader
{
    public IDataSource Datasource { get; }
    public ILocaleDataSource LocaleDatasource { get; }
    
    public IProjectLoader ProjectLoader { get; }
    public ITemplateDatastorePopulator TemplateDatastorePopulator { get; }
    public ILocaleDatastorePopulator LocaleDatastorePopulator { get; }

    public FileDataLoader(IDataSource datasource, ILocaleDataSource localeDatasource, IProjectLoader projectLoader, ITemplateDatastorePopulator templateDatastorePopulator, ILocaleDatastorePopulator localeDatastorePopulator)
    {
        Datasource = datasource;
        LocaleDatasource = localeDatasource;
        ProjectLoader = projectLoader;
        TemplateDatastorePopulator = templateDatastorePopulator;
        LocaleDatastorePopulator = localeDatastorePopulator;
    }

    public virtual async Task<Project> Load(string projectFile)
    {
        var project = await ProjectLoader.LoadProject(projectFile);
        var projectPath = Path.GetDirectoryName(projectFile);
        await TemplateDatastorePopulator.PopulateDatastore(project, projectPath, Datasource);
        await LocaleDatastorePopulator.PopulateDatastore(project, projectPath, LocaleDatasource);
        return project;
    }
}
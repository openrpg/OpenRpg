using OpenRpg.Data;
using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Projects.Extensions;
using OpenRpg.Projects.Loaders.Locales;
using OpenRpg.Projects.Loaders.Projects;
using OpenRpg.Projects.Loaders.Templates;
using OpenRpg.Projects.Models;

namespace OpenRpg.Projects.Loaders;

public class FileDataLoader : IDataLoader
{
    public event EventHandler<ProjectContext>? ProjectLoaded;
    public event EventHandler<ProjectContext>? DataLoaded;
    
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

    public virtual async Task<ProjectContext> Load(string projectFile)
    {
        var project = await ProjectLoader.LoadProject(projectFile);
        var projectContext = project.CreateContext(projectFile);
        
        ProjectLoaded?.Invoke(this, projectContext);
        
        await TemplateDatastorePopulator.PopulateDatastore(projectContext, Datasource);
        await LocaleDatastorePopulator.PopulateDatastore(projectContext, LocaleDatasource);
        DataLoaded?.Invoke(this, projectContext);
        
        return projectContext;
    }
}
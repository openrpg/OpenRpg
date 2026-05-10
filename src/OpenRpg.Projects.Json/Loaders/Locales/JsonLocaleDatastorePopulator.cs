using OpenRpg.Core.Extensions;
using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Projects.Json.Extensions;
using OpenRpg.Projects.Loaders.Locales;
using OpenRpg.Projects.Models;
using OpenRpg.Projects.Services;

namespace OpenRpg.Projects.Json.Loaders.Locales;

public class JsonLocaleDatastorePopulator : ILocaleDatastorePopulator
{
    public IFileService FileService { get; }
    public ILocaleLoader LocaleLoader { get; }

    public JsonLocaleDatastorePopulator(IFileService fileService, ILocaleLoader localeLoader)
    {
        FileService = fileService;
        LocaleLoader = localeLoader;
    }

    public virtual async Task<IReadOnlyCollection<string>> GetAllLocaleFiles(string localePath)
    { return await FileService.GetChildContents(localePath, "*.json"); }

    public async Task PopulateDatastore(Project project, string projectPath, ILocaleDataSource dataSource)
    {
        var localesFolderPath = project.LocalesFolder;
        var absoluteLocaleFolderPath = Path.Combine(projectPath, localesFolderPath);
        var localePathExists = await FileService.Exists(absoluteLocaleFolderPath);
        if(!localePathExists) { throw new Exception($"Locale folder [{absoluteLocaleFolderPath}] cannot be found"); }
        
        var localeFiles = await GetAllLocaleFiles(absoluteLocaleFolderPath);
        foreach (var localeFile in localeFiles)
        {
            var localeDataset = await LocaleLoader.LoadLocales(project, localeFile);
            localeDataset.LocaleData.ForEach(x => AddLocale(localeDataset.LocaleCode, x.Key, x.Value, dataSource));
        }
    }

    public virtual void AddLocale(string localeCode, string localeKey, string localeText, ILocaleDataSource dataSource)
    { dataSource.Update(localeCode, localeKey, localeText); }
}
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenRpg.Localization;
using OpenRpg.Localization.Data.DataSources;
using OpenRpg.Projects.Loaders.Locales;
using OpenRpg.Projects.Models;
using OpenRpg.Projects.Services;

namespace OpenRpg.Projects.Json.Loaders.Locales;

public class JsonLocaleLoader : ILocaleLoader
{
    public IFileService FileService { get; }

    public JsonLocaleLoader(IFileService fileService)
    {
        FileService = fileService;
    }

    public async Task<LocaleDataset> LoadLocales(Project project, string localeFilePath)
    {
        var localePathExists = await FileService.Exists(localeFilePath);
        if(!localePathExists) { throw new Exception($"Locale file [{localeFilePath}] does not exist on file system"); }
        
        var fileContent = await FileService.GetContents(localeFilePath);
        var jsonLocaleData = JObject.Parse(fileContent);

        return LoadLocaleDataset(jsonLocaleData);
    }

    public virtual LocaleDataset LoadLocaleDataset(JObject jsonData)
    {
        var localeData = jsonData.ToObject<LocaleDataset>(new JsonSerializer{ TypeNameHandling = TypeNameHandling.Auto });
        if(localeData is null) { throw new Exception($"Failed to deserialize template data of type [{nameof(LocaleDataset)}]"); }
        return localeData;
    }
}
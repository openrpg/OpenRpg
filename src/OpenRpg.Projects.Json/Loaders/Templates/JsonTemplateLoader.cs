using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenRpg.Core.Templates;
using OpenRpg.Projects.Json.Convertors;
using OpenRpg.Projects.Loaders.Templates;
using OpenRpg.Projects.Models;
using OpenRpg.Projects.Services;

namespace OpenRpg.Projects.Json.Loaders.Templates;

public class JsonTemplateLoader : ITemplateLoader
{
    public IFileService FileService { get; }
    
    public JsonSerializer JsonSerializer { get; } = new()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        Converters = { new VariablesConverter() }
    };

    public JsonTemplateLoader(IFileService fileService)
    {
        FileService = fileService;
    }

    public async Task<IReadOnlyCollection<T>> LoadTemplates<T>(Project project, string templateFilePath) where T : ITemplate
    {
        var templatePathExists = await FileService.Exists(templateFilePath);
        if(!templatePathExists) { throw new Exception($"Template file [{templateFilePath}] does not exist on file system"); }
        
        var fileContent = await FileService.GetContents(templateFilePath);
        var jsonTemplateData = JArray.Parse(fileContent);
        return await ProcessAllTemplateData<T>(project, jsonTemplateData);
    }

    public virtual async Task<IReadOnlyCollection<T>> ProcessAllTemplateData<T>(Project project, JArray jsonTemplates)
        where T : ITemplate
    {
        var templates = new List<T>();
        foreach (var templateData in jsonTemplates)
        {
            var template = ProcessTemplateData<T>(templateData);
            templates.Add(template);
        }
        return templates;
    }
    
    public virtual T ProcessTemplateData<T>(JToken templateData) where T : ITemplate
    {
        var template = templateData.ToObject<T>(JsonSerializer);
        if(template is null) { throw new Exception($"Failed to deserialize template data of type [{typeof(T).Name}] - {templateData}"); }
        return template;
    }
}
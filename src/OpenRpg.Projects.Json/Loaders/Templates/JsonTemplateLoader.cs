using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenRpg.Core.Effects;
using OpenRpg.Core.Requirements;
using OpenRpg.Core.Templates;
using OpenRpg.Core.Variables;
using OpenRpg.Entities.Types;
using OpenRpg.Projects.Loaders.Templates;
using OpenRpg.Projects.Models;
using OpenRpg.Projects.Services;

namespace OpenRpg.Projects.Json.Loaders.Templates;

public class JsonTemplateLoader : ITemplateLoader
{
    public IFileService FileService { get; }
    public JsonSerializer JsonSerializer { get; } = new() { TypeNameHandling = TypeNameHandling.Auto };

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
        
        var dynamicVariables = GetDynamicVariables(template);
        if (dynamicVariables is null) { return template; }

        ProcessKnownVariables(dynamicVariables);
        foreach (var variable in dynamicVariables)
        { dynamicVariables[variable.Key] = DynamicConvert(variable.Value)!; }
        
        return template;
    }

    public virtual void ProcessKnownVariables(IKeyedVariables<int, object> dynamicVariables)
    {
        foreach (var variable in dynamicVariables)
        {
            if (variable.Key == CoreTemplateVariableTypes.Effects)
            {
                var jsonArray = (variable.Value as JArray);
                dynamicVariables[variable.Key] = jsonArray.ToObject<IReadOnlyCollection<IEffect>>(JsonSerializer);
                continue;
            }
            
            if(variable.Key == CoreTemplateVariableTypes.Requirements)
            {
                var jsonArray = (variable.Value as JArray);
                dynamicVariables[variable.Key] = jsonArray.ToObject<IReadOnlyCollection<Requirement>>(JsonSerializer);
                continue;
            }
        }
    }

    public IKeyedVariables<int, object>? GetDynamicVariables<T>(T template) where T : ITemplate
    {
        var propertyName = nameof(ITemplate<>.Variables);
        var templateType = template.GetType();
        var property = templateType.GetProperty(propertyName);
        if(property is null) { return null; }
        
        var variables = property.GetValue(template) as IKeyedVariables<int, object>;
        return variables;
    }
    
    public object? DynamicConvert(object? value)
    {
        return value switch
        {
            JObject obj => obj.ToObject<object>(JsonSerializer),
            JArray arr => arr.Select(DynamicConvert).ToArray(),
            JValue val => val.Value,
            _ => value
        };
    }
}
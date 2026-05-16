using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenRpg.Core.Templates;
using OpenRpg.Editor.Core.Extensions;
using OpenRpg.Editor.Core.Models;
using OpenRpg.Editor.Infrastructure.Data;
using OpenRpg.Editor.Infrastructure.Extensions;
using OpenRpg.Editor.Infrastructure.Plugins;
using OpenRpg.Items.Templates;
using OpenRpg.Items.TradeSkills.Templates;
using OpenRpg.Projects.Json.Convertors;
using OpenRpg.Quests;

namespace OpenRpg.Editor.Infrastructure.Persistence;

public class SaveProjectExecutor
{
    public EditorState EditorState { get; }
    public EditorDatasource EditorDatasource { get; }
    public EditorLocaleDatasource EditorLocaleDatasource { get; }
    public GenreService GenreService { get; }
    
    public SaveProjectExecutor(EditorState editorState, EditorDatasource editorDatasource, EditorLocaleDatasource editorLocaleDatasource, GenreService genreService)
    {
        EditorState = editorState;
        EditorDatasource = editorDatasource;
        EditorLocaleDatasource = editorLocaleDatasource;
        GenreService = genreService;
    }
    
    public async Task Execute()
    {
        if (EditorState.CurrentProject == null)
        { throw new Exception("No project loaded"); }
        
        if(string.IsNullOrEmpty(EditorState.CurrentProject?.ProjectPath))
        { throw new Exception("Folder path is empty"); }
        
        if(!Directory.Exists(EditorState.CurrentProject.TemplatePath))
        { throw new Exception("Data path does not exist on file system"); }

        await SaveAllTemplateTypes();

        await SaveLocaleData();
        await SaveProject();
    }

    private async Task SaveAllTemplateTypes()
    {
        var templateTypeRegistry = GenreService.GetTemplateTypeRegistry();
        var templateTypes = templateTypeRegistry.GetTemplateTypes();
        var saveMethod = typeof(SaveProjectExecutor).GetMethod(nameof(SaveTemplateData), BindingFlags.Instance | BindingFlags.Public);

        foreach (var templateType in templateTypes)
        {
            var classType = templateTypeRegistry.GetTemplateClassType(templateType);
            if (!EditorDatasource.Database.TryGetValue(classType, out var store) || store.Count == 0)
            { continue; }

            var genericMethod = saveMethod.MakeGenericMethod(classType);
            var task = (Task)genericMethod.Invoke(this, []);
            if (task == null) { continue; }
            await task;
        }
    }

    public async Task SaveTemplateData<T>() where T : ITemplate
    {
        if (EditorState.CurrentProject == null)
        { throw new Exception("No project loaded"); }
        
        if(string.IsNullOrEmpty(EditorState.CurrentProject?.ProjectPath))
        { throw new Exception("Folder path is empty"); }
        
        if(!Directory.Exists(EditorState.CurrentProject.TemplatePath))
        { throw new Exception("Data path does not exist on file system"); }

        var data = EditorDatasource.SerializeData<T>();
        var dataFile = $"{EditorState.CurrentProject.TemplatePath}/{typeof(T).Name}.json";
        await File.WriteAllTextAsync(dataFile, data);
    }

    public async Task SaveLocaleData()
    {
        if (EditorState.CurrentProject == null)
        { throw new Exception("No project loaded"); }
        
        if(string.IsNullOrEmpty(EditorState.CurrentProject?.ProjectPath))
        { throw new Exception("Folder path is empty"); }
        
        if(!Directory.Exists(EditorState.CurrentProject.LocalePath))
        { throw new Exception("Locale path does not exist on file system"); }

        foreach (var localeData in EditorLocaleDatasource.LocaleDatasets)
        {
            var data = localeData.Value.SerializeData();
            var dataFile = $"{EditorState.CurrentProject.LocalePath}/{localeData.Key}.json";
            await File.WriteAllTextAsync(dataFile, data);
        }
    }
    
    public async Task SaveProject()
    {
        if (EditorState.CurrentProject == null)
        { throw new Exception("No project loaded"); }
        
        if(string.IsNullOrEmpty(EditorState.CurrentProject?.ProjectPath))
        { throw new Exception("Folder path is empty"); }
        
        var data = JsonConvert.SerializeObject(EditorState.CurrentProject.Project, Formatting.Indented);
        await File.WriteAllTextAsync(EditorState.CurrentProject.GetProjectFilePath(), data);
    }
}
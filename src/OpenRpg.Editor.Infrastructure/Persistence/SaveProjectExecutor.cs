using System;
using System.IO;
using System.Threading.Tasks;
using OpenRpg.Core.Templates;
using OpenRpg.Editor.Core.Extensions;
using OpenRpg.Editor.Core.Models;
using OpenRpg.Editor.Infrastructure.Data;
using OpenRpg.Editor.Infrastructure.Extensions;
using OpenRpg.Entities.Classes.Templates;
using OpenRpg.Entities.Races.Templates;
using OpenRpg.Items.Templates;
using OpenRpg.Quests;

namespace OpenRpg.Editor.Infrastructure.Persistence;

public class SaveProjectExecutor
{
    public EditorState EditorState { get; }
    public EditorDatasource EditorDatasource { get; }
    public EditorLocaleDatasource EditorLocaleDatasource { get; }
    
    public SaveProjectExecutor(EditorState editorState, EditorDatasource editorDatasource, EditorLocaleDatasource editorLocaleDatasource)
    {
        EditorState = editorState;
        EditorDatasource = editorDatasource;
        EditorLocaleDatasource = editorLocaleDatasource;
    }
    
    public async Task Execute()
    {
        if (EditorState.Project == null)
        { throw new Exception("No project loaded"); }
        
        if(string.IsNullOrEmpty(EditorState.Project?.ProjectPath))
        { throw new Exception("Folder path is empty"); }
        
        if(!Directory.Exists(EditorState.Project.GetTemplatePath()))
        { throw new Exception("Data path does not exist on file system"); }

        await SaveTemplateData<ItemTemplate>();
        await SaveTemplateData<ClassTemplate>();
        await SaveTemplateData<RaceTemplate>();
        await SaveTemplateData<Quest>();

        await SaveLocaleData();
    }

    public async Task SaveTemplateData<T>() where T : ITemplate
    {
        if (EditorState.Project == null)
        { throw new Exception("No project loaded"); }
        
        if(string.IsNullOrEmpty(EditorState.Project?.ProjectPath))
        { throw new Exception("Folder path is empty"); }
        
        if(!Directory.Exists(EditorState.Project.GetTemplatePath()))
        { throw new Exception("Data path does not exist on file system"); }

        var data = EditorDatasource.SerializeData<T>();
        var dataFile = $"{EditorState.Project.GetTemplatePath()}/{typeof(T).Name}.json";
        await File.WriteAllTextAsync(dataFile, data);
    }

    public async Task SaveLocaleData()
    {
        if (EditorState.Project == null)
        { throw new Exception("No project loaded"); }
        
        if(string.IsNullOrEmpty(EditorState.Project?.ProjectPath))
        { throw new Exception("Folder path is empty"); }
        
        if(!Directory.Exists(EditorState.Project.GetLocalePath()))
        { throw new Exception("Locale path does not exist on file system"); }

        foreach (var localeData in EditorLocaleDatasource.LocaleDatasets)
        {
            var data = localeData.Value.SerializeData();
            var dataFile = $"{EditorState.Project.GetLocalePath()}/{localeData.Key}.json";
            await File.WriteAllTextAsync(dataFile, data);
        }
    }
}
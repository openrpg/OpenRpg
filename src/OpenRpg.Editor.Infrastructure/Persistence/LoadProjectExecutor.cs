using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

public class LoadProjectExecutor
{
    public EditorState EditorState { get; }
    public EditorDatasource EditorDatasource { get; }
    public EditorLocaleDatasource EditorLocaleDatasource { get; }
    
    public LoadProjectExecutor(EditorState editorState, EditorDatasource editorDatasource, EditorLocaleDatasource editorLocaleDatasource)
    {
        EditorState = editorState;
        EditorDatasource = editorDatasource;
        EditorLocaleDatasource = editorLocaleDatasource;
    }
    
    public async Task Execute(string projectFile)
    {
        if(!File.Exists(projectFile))
        { throw new Exception($"Project file cannot be found in path [{projectFile}]"); }
        
        var projectData = await File.ReadAllTextAsync(projectFile);
        var project = JsonConvert.DeserializeObject<Project>(projectData);
        var loadedProject = new LoadedProject { Project = project, ProjectPath = Path.GetDirectoryName(projectFile) };

        EditorState.Project = loadedProject;

        await RefreshTemplateData<ItemTemplate>();
        await RefreshTemplateData<ClassTemplate>();
        await RefreshTemplateData<RaceTemplate>();
        await RefreshTemplateData<Quest>();

        await RefreshLocaleData();
    }

    public async Task RefreshTemplateData<T>() where T : ITemplate
    {
        if (EditorState.Project == null)
        { throw new Exception("No project loaded"); }
        
        if(string.IsNullOrEmpty(EditorState.Project?.ProjectPath))
        { throw new Exception("Folder path is empty"); }
        
        if(!Directory.Exists(EditorState.Project.GetTemplatePath()))
        { throw new Exception("Template path does not exist on file system"); }

        var dataFile = $"{EditorState.Project.GetTemplatePath()}/{typeof(T).Name}.json";
        var fileContent = await File.ReadAllTextAsync(dataFile);
        EditorDatasource.DeserializeData<T>(fileContent, true);
    }

    public async Task RefreshLocaleData()
    {
        if (EditorState.Project == null)
        { throw new Exception("No project loaded"); }
        
        if(string.IsNullOrEmpty(EditorState.Project?.ProjectPath))
        { throw new Exception("Folder path is empty"); }
        
        if(!Directory.Exists(EditorState.Project.GetLocalePath()))
        { throw new Exception("Locale path does not exist on file system"); }
        
        var localeFiles = Directory.GetFiles(EditorState.Project.GetLocalePath(), "*.json");
        foreach (var localeFile in localeFiles)
        {
            var localeData = await File.ReadAllTextAsync(localeFile);
            EditorLocaleDatasource.DeserializeData(localeData, true);
        }

    }
}
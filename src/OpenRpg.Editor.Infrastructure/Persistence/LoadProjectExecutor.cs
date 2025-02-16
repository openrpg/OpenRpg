using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenRpg.Core.Templates;
using OpenRpg.Editor.Core.Extensions;
using OpenRpg.Editor.Core.Models;
using OpenRpg.Editor.Infrastructure.Data;
using OpenRpg.Editor.Infrastructure.Extensions;
using OpenRpg.Editor.Infrastructure.Persistence.Migrations;
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
    public IEnumerable<IProjectMigration> ProjectMigrations { get; }
    
    public LoadProjectExecutor(EditorState editorState, EditorDatasource editorDatasource, EditorLocaleDatasource editorLocaleDatasource, IEnumerable<IProjectMigration> projectMigrations)
    {
        EditorState = editorState;
        EditorDatasource = editorDatasource;
        EditorLocaleDatasource = editorLocaleDatasource;
        ProjectMigrations = projectMigrations;
    }
    
    public async Task Execute(string projectFile)
    {
        if(!File.Exists(projectFile))
        { throw new Exception($"Project file cannot be found in path [{projectFile}]"); }
        
        var projectData = await File.ReadAllTextAsync(projectFile);
        var project = JsonConvert.DeserializeObject<Project>(projectData);
        var loadedProject = new LoadedProject { Project = project, ProjectPath = Path.GetDirectoryName(projectFile) };

        EditorState.CurrentProject = loadedProject;

        await RefreshTemplateData<ItemTemplate>();
        await RefreshTemplateData<ClassTemplate>();
        await RefreshTemplateData<RaceTemplate>();
        await RefreshTemplateData<Quest>();

        await RefreshLocaleData();
        
        project.Version = ProjectMigrations.OrderBy(x => x.TargetVersion).Last().NewVersion;
    }

    public async Task RefreshTemplateData<T>() where T : ITemplate
    {
        if (EditorState.CurrentProject == null)
        { throw new Exception("No project loaded"); }
        
        if(string.IsNullOrEmpty(EditorState.CurrentProject?.ProjectPath))
        { throw new Exception("Folder path is empty"); }
        
        if(!Directory.Exists(EditorState.CurrentProject.GetTemplatePath()))
        { throw new Exception("Template path does not exist on file system"); }

        var dataFile = $"{EditorState.CurrentProject.GetTemplatePath()}/{typeof(T).Name}.json";
        var fileContent = await File.ReadAllTextAsync(dataFile);
        var jsonData = JArray.Parse(fileContent);
        
        var currentVersion = EditorState.CurrentProject.Project.Version;
        foreach (var migration in ProjectMigrations.OrderBy(x => x.TargetVersion))
        {
            if (migration.TargetVersion == currentVersion)
            {
                jsonData = migration.MigrateTemplate<T>(jsonData);
                currentVersion = migration.NewVersion;
            }
        }
       
        EditorDatasource.DeserializeData<T>(jsonData, true);
    }

    public async Task RefreshLocaleData()
    {
        if (EditorState.CurrentProject == null)
        { throw new Exception("No project loaded"); }
        
        if(string.IsNullOrEmpty(EditorState.CurrentProject?.ProjectPath))
        { throw new Exception("Folder path is empty"); }
        
        if(!Directory.Exists(EditorState.CurrentProject.GetLocalePath()))
        { throw new Exception("Locale path does not exist on file system"); }
        
        var localeFiles = Directory.GetFiles(EditorState.CurrentProject.GetLocalePath(), "*.json");
        foreach (var localeFile in localeFiles)
        {
            var localeData = await File.ReadAllTextAsync(localeFile);
            var jsonData = JObject.Parse(localeData);

            var currentVersion = EditorState.CurrentProject.Project.Version;
            foreach (var migration in ProjectMigrations.OrderBy(x => x.TargetVersion))
            {
                if (migration.TargetVersion == currentVersion)
                {
                    jsonData = migration.MigrateLocale(jsonData);
                    currentVersion = migration.NewVersion;
                }
            }
            
            EditorLocaleDatasource.DeserializeData(jsonData, true);
        }
    }
}
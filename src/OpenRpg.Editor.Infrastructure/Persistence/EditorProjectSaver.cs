using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenRpg.Core.Templates;
using OpenRpg.Editor.Core.Extensions;
using OpenRpg.Editor.Core.Models;
using OpenRpg.Editor.Infrastructure.Data;
using OpenRpg.Editor.Infrastructure.Extensions;
using OpenRpg.Editor.Infrastructure.Plugins;
using OpenRpg.Editor.Infrastructure.Services.Generators;
using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Infrastructure.Persistence;

public class EditorProjectSaver
{
    private readonly ILogger<EditorProjectSaver> _logger;

    event EventHandler<ProjectContext>? ProjectSaved;
    event EventHandler<ProjectContext>? TemplatesSaved;
    event EventHandler<ProjectContext>? LocalesSaved;

    public EditorState EditorState { get; }
    public EditorDatasource EditorDatasource { get; }
    public EditorLocaleDatasource EditorLocaleDatasource { get; }
    public GenreService GenreService { get; }
    public IProjectFileGenerator ProjectFileGenerator { get; }

    public EditorProjectSaver(EditorState editorState, EditorDatasource editorDatasource, EditorLocaleDatasource editorLocaleDatasource, GenreService genreService, IProjectFileGenerator projectFileGenerator, ILogger<EditorProjectSaver> logger)
    {
        EditorState = editorState;
        EditorDatasource = editorDatasource;
        EditorLocaleDatasource = editorLocaleDatasource;
        GenreService = genreService;
        ProjectFileGenerator = projectFileGenerator;
        _logger = logger;
    }
    
    public async Task SaveData()
    {
        if (EditorState.ProjectContext == null)
        { throw new Exception("No project loaded"); }

        if(string.IsNullOrEmpty(EditorState.ProjectContext?.ProjectPath))
        { throw new Exception("Folder path is empty"); }

        if(!Directory.Exists(EditorState.ProjectContext.TemplatePath))
        { throw new Exception("Data path does not exist on file system"); }

        _logger.LogInformation("Starting full save for project at '{ProjectPath}'", EditorState.ProjectContext.ProjectPath);

        await SaveAllTemplateTypes();
        TemplatesSaved?.Invoke(this, EditorState.ProjectContext);

        await SaveLocaleData();
        LocalesSaved?.Invoke(this, EditorState.ProjectContext);

        await SaveProject();
        ProjectSaved?.Invoke(this, EditorState.ProjectContext);

        await GenerateProjectClasses();

        _logger.LogInformation("Project save complete");
    }

    private async Task SaveAllTemplateTypes()
    {
        var templateTypeRegistry = GenreService.GetTemplateTypeRegistry();
        var templateTypes = templateTypeRegistry.GetTemplateTypes();
        var saveMethod = typeof(EditorProjectSaver).GetMethod(nameof(SaveTemplateData), BindingFlags.Instance | BindingFlags.Public);

        foreach (var templateType in templateTypes)
        {
            var classType = templateTypeRegistry.GetTemplateClassType(templateType);
            if (!EditorDatasource.Database.TryGetValue(classType, out var store) || store.Count == 0)
            {
                _logger.LogDebug("Skipping empty template type '{TemplateTypeKey}' ({ClassName})", templateType.Key, classType?.Name);
                continue;
            }

            _logger.LogInformation("Saving {Count} {ClassName} entries", store.Count, classType?.Name);
            var genericMethod = saveMethod.MakeGenericMethod(classType);
            var task = (Task)genericMethod.Invoke(this, []);
            if (task == null) { continue; }
            await task;
        }
    }

    public async Task SaveTemplateData<T>() where T : ITemplate
    {
        if (EditorState.ProjectContext == null)
        { throw new Exception("No project loaded"); }

        if(string.IsNullOrEmpty(EditorState.ProjectContext?.ProjectPath))
        { throw new Exception("Folder path is empty"); }

        if(!Directory.Exists(EditorState.ProjectContext.TemplatePath))
        { throw new Exception("Data path does not exist on file system"); }

        var data = EditorDatasource.SerializeData<T>();
        var dataFile = $"{EditorState.ProjectContext.TemplatePath}/{typeof(T).Name}.json";
        await File.WriteAllTextAsync(dataFile, data);

        _logger.LogDebug("Wrote {ClassName} data to '{DataFile}'", typeof(T).Name, dataFile);
    }

    public async Task SaveLocaleData()
    {
        if (EditorState.ProjectContext == null)
        { throw new Exception("No project loaded"); }

        if(string.IsNullOrEmpty(EditorState.ProjectContext?.ProjectPath))
        { throw new Exception("Folder path is empty"); }

        if(!Directory.Exists(EditorState.ProjectContext.LocalePath))
        { throw new Exception("Locale path does not exist on file system"); }

        foreach (var localeData in EditorLocaleDatasource.LocaleDatasets)
        {
            var data = localeData.Value.SerializeData();
            var dataFile = $"{EditorState.ProjectContext.LocalePath}/{localeData.Key}.json";
            await File.WriteAllTextAsync(dataFile, data);
            _logger.LogDebug("Wrote locale data '{LocaleKey}' to '{DataFile}'", localeData.Key, dataFile);
        }
    }

    public async Task SaveProject()
    {
        if (EditorState.ProjectContext == null)
        { throw new Exception("No project loaded"); }

        if(string.IsNullOrEmpty(EditorState.ProjectContext?.ProjectPath))
        { throw new Exception("Folder path is empty"); }

        var data = JsonConvert.SerializeObject(EditorState.ProjectContext.Project, Formatting.Indented);
        await File.WriteAllTextAsync(EditorState.ProjectContext.GetProjectFilePath(), data);

        _logger.LogDebug("Wrote project file to '{ProjectFilePath}'", EditorState.ProjectContext.GetProjectFilePath());
    }

    public async Task GenerateProjectClasses()
    {
        if (!EditorState.ProjectContext.Project.GenerateTypeFiles)
        {
            _logger.LogDebug("Type file generation is disabled, skipping");
            return;
        }

        var generatedFiles = ProjectFileGenerator.GenerateFiles(EditorState.ProjectContext, EditorDatasource);
        _logger.LogInformation("Generating {FileCount} project class file(s)", generatedFiles.Count);

        foreach (var generatedFile in generatedFiles)
        {
            var filePath = Path.IsPathFullyQualified(EditorState.ProjectContext.Project.GeneratedFilePath)
                ? Path.Combine(EditorState.ProjectContext.Project.GeneratedFilePath, generatedFile.Path, generatedFile.Filename)
                : Path.Combine(EditorState.ProjectContext.ProjectPath, EditorState.ProjectContext.Project.GeneratedFilePath, generatedFile.Path, generatedFile.Filename);

            var dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
            await File.WriteAllTextAsync(filePath, generatedFile.Content);

            _logger.LogInformation("Generated class file: '{FilePath}'", filePath);
        }
    }
}
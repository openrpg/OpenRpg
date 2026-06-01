using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenRpg.Editor.Core.Extensions;
using OpenRpg.Editor.Infrastructure.Plugins;
using OpenRpg.Projects.Extensions;
using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Infrastructure.Persistence;

public class ProjectCreator
{
    private readonly GenreService _genreService;
    private readonly ILogger<ProjectCreator> _logger;

    public ProjectCreator(GenreService genreService, ILogger<ProjectCreator> logger)
    {
        _genreService = genreService;
        _logger = logger;
    }

    public async Task<ProjectContext> CreateProjectAt(string folderPath, IEnumerable<string> enabledGenreIds = null, bool generateTypeFiles = false)
    {
        if(string.IsNullOrEmpty(folderPath))
        { throw new ArgumentException("Folder path is empty", nameof(folderPath)); }

        _logger.LogInformation("Creating project at '{FolderPath}' (generateTypeFiles: {GenerateTypeFiles})", folderPath, generateTypeFiles);

        var plugins = new List<PluginDescriptor>();
        if (enabledGenreIds != null)
        {
            foreach (var id in enabledGenreIds)
            {
                var plugin = _genreService.AvailablePlugins.FirstOrDefault(p => p.PluginId == id);
                if (plugin != null)
                {
                    plugins.Add(new PluginDescriptor { Id = plugin.PluginId, Version = plugin.Version });
                }
                else
                {
                    _logger.LogWarning("Requested plugin '{PluginId}' is not available in the loaded plugins", id);
                    plugins.Add(new PluginDescriptor { Id = id });
                }
            }
        }

        var newProject = new Project { Plugins = plugins };
        newProject.GenerateTypeFiles = generateTypeFiles;
        var projectFile = $"{folderPath}/project.json";

        var projectContent = JsonConvert.SerializeObject(newProject, Formatting.Indented);
        await File.WriteAllTextAsync(projectFile, projectContent);

        var projectContext = newProject.CreateContext(projectFile);
        Directory.CreateDirectory(projectContext.TemplatePath);
        Directory.CreateDirectory(projectContext.AssetPath);
        Directory.CreateDirectory(projectContext.LocalePath);

        if(projectContext.Project.GenerateTypeFiles)
        {
            Directory.CreateDirectory(projectContext.GeneratedClassPath);
            _logger.LogInformation("Created generated classes directory at '{Path}'", projectContext.GeneratedClassPath);
        }

        _logger.LogInformation("Project created successfully at '{FolderPath}' with {PluginCount} plugin(s)", folderPath, plugins.Count);
        return projectContext;
    }
}
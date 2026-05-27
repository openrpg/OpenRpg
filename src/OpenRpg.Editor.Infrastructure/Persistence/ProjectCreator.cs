using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenRpg.Editor.Core.Extensions;
using OpenRpg.Editor.Infrastructure.Plugins;
using OpenRpg.Projects.Extensions;
using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Infrastructure.Persistence;

public class ProjectCreator(GenreService GenreService)
{
    public async Task<ProjectContext> CreateProjectAt(string folderPath, IEnumerable<string> enabledGenreIds = null)
    {
        if(string.IsNullOrEmpty(folderPath))
        { throw new ArgumentException("Folder path is empty", nameof(folderPath)); }

        var plugins = new List<PluginDescriptor>();
        if (enabledGenreIds != null)
        {
            foreach (var id in enabledGenreIds)
            {
                var plugin = GenreService.AvailablePlugins.FirstOrDefault(p => p.PluginId == id);
                plugins.Add(plugin != null
                    ? new PluginDescriptor { Id = plugin.PluginId, Version = plugin.Version }
                    : new PluginDescriptor { Id = id });
            }
        }

        var newProject = new Project { Plugins = plugins };
        var projectFile = $"{folderPath}/project.json";
        var projectContent = JsonConvert.SerializeObject(newProject, Formatting.Indented);
        
        var projectContext = newProject.CreateContext(projectFile);
        
        await File.WriteAllTextAsync(projectFile, projectContent);
        Directory.CreateDirectory(projectContext.TemplatePath);
        Directory.CreateDirectory(projectContext.AssetPath);
        Directory.CreateDirectory(projectContext.LocalePath);
        
        if(projectContext.Project.GenerateTypeFiles)
        { Directory.CreateDirectory(projectContext.GeneratedClassPath); }
        
        return projectContext;
    }
}
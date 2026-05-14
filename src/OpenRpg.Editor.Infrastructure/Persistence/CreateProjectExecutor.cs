using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenRpg.Editor.Core.Extensions;
using OpenRpg.Editor.Core.Models;
using OpenRpg.Editor.Infrastructure.Plugins;
using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Infrastructure.Persistence;

public class CreateProjectExecutor(EditorState EditorState, GenreService GenreService)
{
    public async Task<LoadedProject> Execute(string folderPath, IEnumerable<string> enabledGenreIds = null)
    {
        if(string.IsNullOrEmpty(folderPath))
        { throw new ArgumentException("Folder path is empty", nameof(folderPath)); }

        var plugins = new List<PluginDescriptor>();
        if (enabledGenreIds != null)
        {
            foreach (var id in enabledGenreIds)
            {
                var plugin = GenreService.AvailablePlugins.FirstOrDefault(p => p.PluginId == id);
                if (plugin != null)
                {
                    plugins.Add(new PluginDescriptor { Id = plugin.PluginId, Version = plugin.Version });
                }
                else
                {
                    plugins.Add(new PluginDescriptor { Id = id });
                }
            }
        }
        
        var newProject = new LoadedProject { ProjectPath = folderPath };
        newProject.Project.Plugins = plugins;
        
        var projectFile = $"{folderPath}/project.json";
        var projectContent = JsonConvert.SerializeObject(newProject.Project, Formatting.Indented);

        await File.WriteAllTextAsync(projectFile, projectContent);
        Directory.CreateDirectory(newProject.TemplatePath);
        Directory.CreateDirectory(newProject.AssetPath);
        Directory.CreateDirectory(newProject.LocalePath);
        Directory.CreateDirectory(newProject.GetAssetPath("items"));
        
        return newProject;
    }
}
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenRpg.Editor.Core.Extensions;
using OpenRpg.Editor.Core.Models;

namespace OpenRpg.Editor.Infrastructure.Persistence;

public class CreateProjectExecutor(EditorState EditorState)
{
    public async Task<LoadedProject> Execute(string folderPath)
    {
        if(string.IsNullOrEmpty(folderPath))
        { throw new ArgumentException("Folder path is empty", nameof(folderPath)); }
        
        var newProject = new LoadedProject { ProjectPath = folderPath };
        var projectFile = $"{folderPath}/project.json";
        var projectContent = JsonConvert.SerializeObject(newProject.Project, Formatting.Indented);

        await File.WriteAllTextAsync(projectFile, projectContent);
        Directory.CreateDirectory(newProject.GetTemplatePath());
        Directory.CreateDirectory(newProject.GetAssetPath());
        Directory.CreateDirectory(newProject.GetLocalePath());
        Directory.CreateDirectory(newProject.GetAssetPath("items"));
        
        return newProject;
    }
}
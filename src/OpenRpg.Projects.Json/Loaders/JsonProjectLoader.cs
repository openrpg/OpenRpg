using Newtonsoft.Json;
using OpenRpg.Projects.Loaders;
using OpenRpg.Projects.Loaders.Projects;
using OpenRpg.Projects.Models;
using OpenRpg.Projects.Services;

namespace OpenRpg.Projects.Json.Loaders;

public class JsonProjectLoader : IProjectLoader
{
    public IFileService FileService { get; }

    public JsonProjectLoader(IFileService fileService)
    {
        FileService = fileService;
    }

    public async Task<Project> LoadProject(string projectFilePath)
    {
        var projectExists = await FileService.Exists(projectFilePath);
        if(!projectExists) { throw new Exception($"Project file cannot be found in path [{projectFilePath}]"); }
        
        var projectData = await FileService.GetContents(projectFilePath);
        var project = JsonConvert.DeserializeObject<Project>(projectData);
        
        return project ?? throw new Exception("Project data cannot be read");
    }
}
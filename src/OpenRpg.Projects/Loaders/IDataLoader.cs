using OpenRpg.Projects.Models;

namespace OpenRpg.Projects.Loaders;

/// <summary>
/// This acts as a high level loader for all project and template/locale data related to a project
/// </summary>
public interface IDataLoader
{
    event EventHandler<ProjectContext>? ProjectLoaded;
    event EventHandler<ProjectContext>? DataLoaded;
    
    Task<ProjectContext> Load(string projectFile);
}
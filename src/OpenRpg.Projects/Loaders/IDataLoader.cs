using OpenRpg.Projects.Models;

namespace OpenRpg.Projects.Loaders;

/// <summary>
/// This acts as a high level loader for all data related to a project
/// </summary>
public interface IDataLoader
{
    Task<Project> Load(string projectFile);
}
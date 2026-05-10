using OpenRpg.Projects.Models;

namespace OpenRpg.Projects.Loaders.Projects;

public interface IProjectLoader
{
    /// <summary>
    /// This should load the project from a given reference
    /// </summary>
    /// <param name="projectReference">The reference to the project data, be it a string file path or asset name</param>
    /// <returns>The project data</returns>
    Task<Project> LoadProject(string projectReference);
}
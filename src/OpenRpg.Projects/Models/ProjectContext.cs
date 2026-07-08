namespace OpenRpg.Projects.Models;

public class ProjectContext
{
    /// <summary>
    /// The loaded project data
    /// </summary>
    public required Project Project { get; init; }
    
    /// <summary>
    /// The path/handle to the project
    /// </summary>
    /// <remarks>This may not actually be a file path, it could be a resource handle or an asset name for in engine bundled assets</remarks>
    public required string ProjectFile { get; init; }
    
    /// <summary>
    /// The path to the project file if available
    /// </summary>
    /// <remarks>This may not be available for all project types</remarks>
    public required string ProjectPath { get; init; }
}
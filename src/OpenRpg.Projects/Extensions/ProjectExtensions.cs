using OpenRpg.Projects.Models;

namespace OpenRpg.Projects.Extensions;

public static class ProjectExtensions
{
    extension(Project project)
    {
        public ProjectContext CreateContext(string projectFile)
        {
            if(string.IsNullOrEmpty(projectFile)) { throw new Exception("Project file is empty"); }
            var projectPath = Path.Exists(projectFile) ? Path.GetDirectoryName(projectFile) ?? string.Empty : string.Empty;
            
            return new ProjectContext
            {
                Project = project,
                ProjectFile = projectFile,
                ProjectPath = projectPath
            };
        }
    }
}
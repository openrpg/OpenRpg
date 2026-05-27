using System.Collections.Generic;
using OpenRpg.Data;
using OpenRpg.Editor.Core.Models;
using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Core.Services.Generators;

public class ProjectFileGenerator : IProjectFileGenerator
{
    public IReadOnlyCollection<GeneratedFile> GenerateFiles(ProjectContext context, IDataSource dataSource)
    {
        return new List<GeneratedFile>();
    }
}
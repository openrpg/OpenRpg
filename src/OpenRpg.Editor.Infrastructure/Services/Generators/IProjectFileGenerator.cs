using System.Collections.Generic;
using OpenRpg.Editor.Core.Models;
using OpenRpg.Editor.Infrastructure.Data;
using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Infrastructure.Services.Generators;

public interface IProjectFileGenerator
{
    IReadOnlyCollection<GeneratedFile> GenerateFiles(ProjectContext context, EditorDatasource dataSource);
}
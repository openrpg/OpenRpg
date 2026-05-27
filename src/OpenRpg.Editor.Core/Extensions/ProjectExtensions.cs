using System;
using System.Collections.Generic;
using OpenRpg.Editor.Core.Types;
using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Core.Extensions;

public static class ProjectExtensions
{
    extension(Project project)
    {
        public bool GenerateTypeFiles
        {
            get => project.Metadata.GetValueOrDefault(ProjectMetadataKeys.GenerateTypeFiles, "false").Equals("true", StringComparison.InvariantCultureIgnoreCase);
            set => project.Metadata[ProjectMetadataKeys.GenerateTypeFiles] = value.ToString().ToLowerInvariant();
        }
        
        public string GeneratedFilePath
        {
            get => project.Metadata.GetValueOrDefault(ProjectMetadataKeys.GeneratedFilePath, "types");
            set => project.Metadata[ProjectMetadataKeys.GeneratedFilePath] = value;
        }
    }
}
using OpenRpg.Projects.Json.Types;
using OpenRpg.Projects.Models;

namespace OpenRpg.Projects.Json.Extensions;

public static class ProjectExtensions
{
    extension(Project project)
    {
        public string AssetFolder
        {
            get => project.Metadata.GetValueOrDefault(ProjectMetadataKeys.AssetFolder, "assets");
            set => project.Metadata[ProjectMetadataKeys.AssetFolder] = value;
        }
        
        public string TemplatesFolder
        {
            get => project.Metadata.GetValueOrDefault(ProjectMetadataKeys.TemplatesFolder, "templates");
            set => project.Metadata[ProjectMetadataKeys.TemplatesFolder] = value;
        }
        
        public string LocalesFolder
        {
            get => project.Metadata.GetValueOrDefault(ProjectMetadataKeys.LocalesFolder, "locales");
            set => project.Metadata[ProjectMetadataKeys.LocalesFolder] = value;
        }
    }
}
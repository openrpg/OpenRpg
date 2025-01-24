using OpenRpg.Editor.Core.Models;

namespace OpenRpg.Editor.Core.Extensions;

public static class LoadedProjectExtensions
{
    public static string GetTemplatePath(this LoadedProject project)
    { return $"{project.ProjectPath}/{project.Project.TemplatesFolder}"; }
    
    public static string GetLocalePath(this LoadedProject project)
    { return $"{project.ProjectPath}/{project.Project.LocalesFolder}"; }
    
    public static string GetAssetPath(this LoadedProject project)
    { return $"{project.ProjectPath}/{project.Project.AssetsFolder}"; }
    
    public static string GetAssetPath(this LoadedProject project, string type)
    { return $"{project.ProjectPath}/{project.Project.AssetsFolder}/{type}"; }
    
    public static string GetAssetPath(this LoadedProject project, string type, string assetCode, string extension)
    { return $"{project.ProjectPath}/{project.Project.AssetsFolder}/{type}/{assetCode}.{extension}"; }
}
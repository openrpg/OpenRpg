using System;
using System.IO;
using OpenRpg.Editor.Core.Models;

namespace OpenRpg.Editor.Core.Extensions;

public static class LoadedProjectExtensions
{
    public static string GetTemplatePath(this LoadedProject project)
    {
        var isAbsolutePath = Path.IsPathFullyQualified(project.Project.TemplatesFolder);
        return isAbsolutePath ? project.Project.TemplatesFolder :
            $"{project.ProjectPath}/{project.Project.TemplatesFolder}";
    }

    public static string GetLocalePath(this LoadedProject project)
    {
        var isAbsolutePath = Path.IsPathFullyQualified(project.Project.LocalesFolder);
        return isAbsolutePath ? project.Project.LocalesFolder :
            $"{project.ProjectPath}/{project.Project.LocalesFolder}";
    }

    public static string GetAssetPath(this LoadedProject project)
    {
        var isAbsolutePath = Path.IsPathFullyQualified(project.Project.AssetsFolder);
        return isAbsolutePath ? project.Project.AssetsFolder :
            $"{project.ProjectPath}/{project.Project.AssetsFolder}";
    }
    
    public static string GetAssetPath(this LoadedProject project, string type)
    { return $"{GetAssetPath(project)}/{type}"; }
    
    public static string GetAssetPath(this LoadedProject project, string type, string assetCode, string extension)
    { return $"{GetAssetPath(project, type)}/{assetCode}.{extension}"; }

    public static string GetImageAssetAsDataUrl(this LoadedProject project, string type, string assetCode, string extension)
    {
        var imagePath = GetAssetPath(project, type, assetCode, extension);
        if(!File.Exists(imagePath)) { return string.Empty; }
        
        var binaryData = File.ReadAllBytes(imagePath);
        var base64String = Convert.ToBase64String(binaryData);
        return $"data:image/png;base64,{base64String}";
    }
    
    public static string GetProjectFilePath(this LoadedProject project)
    { return $"{project.ProjectPath}/project.json"; }
}
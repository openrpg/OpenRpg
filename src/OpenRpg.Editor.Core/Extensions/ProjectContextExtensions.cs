using System;
using System.IO;
using OpenRpg.Projects.Json.Extensions;
using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Core.Extensions;

public static class ProjectContextExtensions
{
    extension(ProjectContext projectContext)
    {
        public string TemplatePath => GetTemplatePath(projectContext);
        public string LocalePath => GetLocalePath(projectContext);
        public string AssetPath => GetAssetPath(projectContext);
        public string GeneratedClassPath => GetGeneratedClassPath(projectContext);
        
        public string GetTemplatePath()
        {
            var isAbsolutePath = Path.IsPathFullyQualified(projectContext.Project.TemplatesFolder);
            return isAbsolutePath ? projectContext.Project.TemplatesFolder :
                $"{projectContext.ProjectPath}/{projectContext.Project.TemplatesFolder}";
        }
        
        public string GetLocalePath()
        {
            var isAbsolutePath = Path.IsPathFullyQualified(projectContext.Project.LocalesFolder);
            return isAbsolutePath ? projectContext.Project.LocalesFolder :
                $"{projectContext.ProjectPath}/{projectContext.Project.LocalesFolder}";
        }

        public string GetAssetPath()
        {
            var isAbsolutePath = Path.IsPathFullyQualified(projectContext.Project.AssetFolder);
            return isAbsolutePath ? projectContext.Project.AssetFolder :
                $"{projectContext.ProjectPath}/{projectContext.Project.AssetFolder}";
        }

        public string GetGeneratedClassPath()
        {
            var isAbsolutePath = Path.IsPathFullyQualified(projectContext.Project.GeneratedFilePath);
            return isAbsolutePath ? projectContext.Project.GeneratedFilePath :
                $"{projectContext.ProjectPath}/{projectContext.Project.GeneratedFilePath}";
        }
    
        public string GetAssetPath(string type)
        { return $"{GetAssetPath(projectContext)}/{type}"; }
    
        public string GetAssetPath(string type, string assetCode, string extension)
        { return $"{GetAssetPath(projectContext, type)}/{assetCode}.{extension}"; }

        public string GetImageAssetAsDataUrl(string type, string assetCode, string extension)
        {
            var imagePath = GetAssetPath(projectContext, type, assetCode, extension);
            if(!File.Exists(imagePath)) { return string.Empty; }
        
            var binaryData = File.ReadAllBytes(imagePath);
            var base64String = Convert.ToBase64String(binaryData);
            return $"data:image/png;base64,{base64String}";
        }
    
        public string GetProjectFilePath()
        { return $"{projectContext.ProjectPath}/project.json"; }
    }
}
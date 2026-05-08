using System;

namespace OpenRpg.Editor.Core.Models;

public class Project
{
    public string Version { get; set; } = "1.0.1";
    public string AssetsFolder { get; set; } = "assets";
    public string TemplatesFolder { get; set; } = "templates";
    public string LocalesFolder { get; set; } = "locales";
}
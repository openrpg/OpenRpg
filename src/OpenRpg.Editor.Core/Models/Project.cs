using System;

namespace OpenRpg.Editor.Core.Models;

public class Project
{
    public string Version { get; set; } = "1.0.0";
    public string AssetsFolder { get; set; } = "/assets";
    public string DataFolder { get; set; } = "/data";
}
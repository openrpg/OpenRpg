namespace OpenRpg.Projects.Models;

public class Project
{
    public string Version { get; set; } = "1.0.1";
    public string Type { get; set; } = "json";
    
    public Dictionary<string, string> Metadata { get; set; } = new();
    public IReadOnlyCollection<PluginDescriptor> Plugins { get; set; } = new List<PluginDescriptor>();
}
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenRpg.Editor.Core.Plugins;

public class PluginManifest
{
    [JsonProperty("schemaVersion")]
    public string SchemaVersion { get; set; } = "1.0.0";

    [JsonProperty("pluginId")]
    public string PluginId { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("version")]
    public string Version { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("isDefault")]
    public bool IsDefault { get; set; }

    [JsonProperty("typeSources")]
    public Dictionary<string, string> TypeSources { get; set; } = new();

    [JsonProperty("templateTypes")]
    public Dictionary<string, TemplateTypeEntry> TemplateTypes { get; set; } = new();

    public bool IsValid()
    {
        return !string.IsNullOrEmpty(PluginId) &&
               !string.IsNullOrEmpty(Name) &&
               !string.IsNullOrEmpty(SchemaVersion);
    }
}
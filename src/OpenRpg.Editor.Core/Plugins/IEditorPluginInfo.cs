using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Core.Plugins;

public interface IEditorPluginInfo
{
    string PluginId { get; }
    string Name { get; }
    string Version { get; }
    string Description { get; }
    PluginManifest Manifest { get; }
    IGenreTypesProvider TypesProvider { get; }
    
    PluginDescriptor ToPluginDescriptor();
}
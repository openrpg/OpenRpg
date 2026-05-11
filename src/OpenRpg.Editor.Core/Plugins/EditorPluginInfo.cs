using System.Collections.Generic;
using OpenRpg.Projects.Models;

namespace OpenRpg.Editor.Core.Plugins;

public class EditorPluginInfo : IEditorPluginInfo
{
    public string PluginId => Manifest.PluginId;
    public string Id => PluginId;
    public string Name => Manifest.Name;
    public string Version => Manifest.Version;
    public string Description => Manifest.Description;
    public PluginManifest Manifest { get; }
    public IGenreTypesProvider TypesProvider { get; }
    public IReadOnlyList<ITemplateTypeDescriptor> TemplateTypes { get; }

    public EditorPluginInfo(PluginManifest manifest, IGenreTypesProvider typesProvider)
    {
        Manifest = manifest;
        TypesProvider = typesProvider;

        var templateTypes = new List<ITemplateTypeDescriptor>();
        foreach (var (key, entry) in manifest.TemplateTypes)
        {
            entry.Key = key;
            templateTypes.Add(new TemplateTypeDescriptor(entry));
        }
        TemplateTypes = templateTypes;
    }

    public PluginDescriptor ToPluginDescriptor()
    {
        return new PluginDescriptor
        {
            Id = PluginId,
            Name = Name,
            Version = Version
        };
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using OpenRpg.Editor.Core.Plugins;

namespace OpenRpg.Editor.Infrastructure.Plugins;

public class GenreService
{
    private readonly ManifestPluginLoader _pluginLoader;
    private readonly List<EditorPluginInfo> _enabledPlugins = new();
    private readonly ILogger<GenreService> _logger;
    private bool _initialized = false;
    private ITemplateTypeRegistry _templateTypeRegistry;

    public IReadOnlyList<EditorPluginInfo> AvailablePlugins => _pluginLoader.LoadedPlugins;
    public IReadOnlyList<EditorPluginInfo> EnabledPlugins => _enabledPlugins.AsReadOnly();
    public IReadOnlyList<string> LoadErrors => _pluginLoader.Errors;
    public bool HasLoadErrors => _pluginLoader.HasErrors;

    public GenreService(ManifestPluginLoader pluginLoader, ILogger<GenreService> logger)
    {
        _pluginLoader = pluginLoader;
        _logger = logger;
    }

    private void EnsureInitialized()
    {
        if (_initialized) return;
        _initialized = true;
        _pluginLoader.LoadPlugins();
        RebuildTemplateRegistry();
    }

    private void RebuildTemplateRegistry()
    {
        _templateTypeRegistry = new TemplateTypeRegistry(_enabledPlugins);
    }

    public ITemplateTypeRegistry GetTemplateTypeRegistry()
    {
        EnsureInitialized();
        return _templateTypeRegistry;
    }

    public void RefreshPlugins()
    {
        _logger.LogInformation("Refreshing plugins (reloading all plugin manifests)");
        _pluginLoader.LoadPlugins();
        _enabledPlugins.Clear();
        _initialized = true;
        RebuildTemplateRegistry();
        _logger.LogInformation("Plugin refresh complete: {AvailableCount} available, {EnabledCount} enabled",
            AvailablePlugins.Count, EnabledPlugins.Count);
    }

    public bool EnablePlugin(string pluginId)
    {
        EnsureInitialized();
        var plugin = _pluginLoader.LoadedPlugins.FirstOrDefault(p => p.PluginId == pluginId);
        if (plugin == null)
        {
            _logger.LogWarning("Attempted to enable unknown plugin '{PluginId}'", pluginId);
            return false;
        }

        if (_enabledPlugins.Any(p => p.PluginId == pluginId))
        {
            _logger.LogDebug("Plugin '{PluginId}' is already enabled", pluginId);
            return true;
        }

        _enabledPlugins.Add(plugin);
        RebuildTemplateRegistry();
        _logger.LogInformation("Enabled plugin '{PluginId}' ({PluginName})", pluginId, plugin.Manifest.Name);
        return true;
    }

    public void DisablePlugin(string pluginId)
    {
        var removed = _enabledPlugins.RemoveAll(p => p.PluginId == pluginId);
        RebuildTemplateRegistry();
        if (removed > 0)
        {
            _logger.LogInformation("Disabled plugin '{PluginId}'", pluginId);
        }
    }

    public void SetEnabledPlugins(IEnumerable<string> pluginIds)
    {
        EnsureInitialized();
        var ids = pluginIds.ToList();
        _logger.LogInformation("Setting enabled plugins: [{PluginIds}]", string.Join(", ", ids));
        _enabledPlugins.Clear();
        foreach (var id in ids)
        {
            EnablePlugin(id);
        }
        _logger.LogInformation("Enabled plugins set: {EnabledCount} plugin(s) active", _enabledPlugins.Count);
    }

    public IGenreTypesProvider GetCombinedTypesProvider()
    {
        EnsureInitialized();
        if (_enabledPlugins.Count == 0)
        {
            return new EmptyGenreTypesProvider();
        }
        return new CombinedGenreTypesProvider(_enabledPlugins.Select(p => p.TypesProvider).ToList());
    }

    public IGenreTypesProvider GetLoadedTypesProvider()
    {
        EnsureInitialized();
        if (_pluginLoader.LoadedPlugins.Count == 0)
        {
            return new EmptyGenreTypesProvider();
        }
        return new CombinedGenreTypesProvider(
            _pluginLoader.LoadedPlugins.Select(p => p.TypesProvider).ToList()
        );
    }

    public (bool IsValid, List<string> Conflicts) ValidatePluginCombination(EditorPluginInfo newPlugin)
    {
        EnsureInitialized();
        var conflicts = new List<string>();
        var existingIds = _enabledPlugins
            .SelectMany(p => p.TypesProvider.ItemTypes.Select(t => t.Id))
            .ToHashSet();

        var newIds = newPlugin.TypesProvider.ItemTypes.Select(t => t.Id);
        foreach (var id in newIds)
        {
            if (existingIds.Contains(id))
            {
                conflicts.Add($"Type ID {id} conflict for item types");
            }
        }

        if (conflicts.Count > 0)
        {
            _logger.LogWarning("Plugin '{PluginId}' has {ConflictCount} type ID conflict(s) with currently enabled plugins: {Conflicts}",
                newPlugin.PluginId, conflicts.Count, string.Join("; ", conflicts));
        }

        return (conflicts.Count == 0, conflicts);
    }

    public List<EditorPluginInfo> GetMissingPlugins(IEnumerable<string> requiredPluginIds)
    {
        EnsureInitialized();
        var loadedIds = _pluginLoader.LoadedPlugins.Select(p => p.PluginId).ToHashSet();
        var missing = requiredPluginIds.Where(id => !loadedIds.Contains(id)).ToList();

        if (missing.Count > 0)
        {
            _logger.LogWarning("Project references {MissingCount} plugin(s) that are not loaded: [{MissingPlugins}]",
                missing.Count, string.Join(", ", missing));
        }

        return missing.Select(id => new EditorPluginInfo(new PluginManifest { PluginId = id, Name = id }, new EmptyGenreTypesProvider())).ToList();
    }
}
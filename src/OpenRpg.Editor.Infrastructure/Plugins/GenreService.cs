using System;
using System.Collections.Generic;
using System.Linq;
using OpenRpg.Editor.Core.Plugins;

namespace OpenRpg.Editor.Infrastructure.Plugins;

public class GenreService
{
    private readonly ManifestPluginLoader _pluginLoader;
    private readonly List<EditorPluginInfo> _enabledPlugins = new();
    private bool _initialized = false;
    private ITemplateTypeRegistry _templateTypeRegistry;

    public IReadOnlyList<EditorPluginInfo> AvailablePlugins => _pluginLoader.LoadedPlugins;
    public IReadOnlyList<EditorPluginInfo> EnabledPlugins => _enabledPlugins.AsReadOnly();
    public IReadOnlyList<string> LoadErrors => _pluginLoader.Errors;
    public bool HasLoadErrors => _pluginLoader.HasErrors;

    public GenreService(ManifestPluginLoader pluginLoader)
    {
        _pluginLoader = pluginLoader;
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
        _pluginLoader.LoadPlugins();
        _enabledPlugins.Clear();
        _initialized = true;
        RebuildTemplateRegistry();
    }

    public bool EnablePlugin(string pluginId)
    {
        EnsureInitialized();
        var plugin = _pluginLoader.LoadedPlugins.FirstOrDefault(p => p.PluginId == pluginId);
        if (plugin == null) return false;
        
        if (_enabledPlugins.Any(p => p.PluginId == pluginId))
            return true;

        _enabledPlugins.Add(plugin);
        RebuildTemplateRegistry();
        return true;
    }

    public void DisablePlugin(string pluginId)
    {
        _enabledPlugins.RemoveAll(p => p.PluginId == pluginId);
        RebuildTemplateRegistry();
    }

    public void SetEnabledPlugins(IEnumerable<string> pluginIds)
    {
        EnsureInitialized();
        _enabledPlugins.Clear();
        foreach (var id in pluginIds)
        {
            EnablePlugin(id);
        }
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

        return (conflicts.Count == 0, conflicts);
    }

    public List<EditorPluginInfo> GetMissingPlugins(IEnumerable<string> requiredPluginIds)
    {
        EnsureInitialized();
        var loadedIds = _pluginLoader.LoadedPlugins.Select(p => p.PluginId).ToHashSet();
        return requiredPluginIds.Where(id => !loadedIds.Contains(id)).Select(id => new EditorPluginInfo(new PluginManifest { PluginId = id, Name = id }, new EmptyGenreTypesProvider())).ToList();
    }
}

public class EmptyGenreTypesProvider : IGenreTypesProvider
{
    public string PluginId => "empty";
    public OptionData[] ItemTypes => Array.Empty<OptionData>();
    public OptionData[] ItemQualityTypes => Array.Empty<OptionData>();
    public OptionData[] RequirementTypes => Array.Empty<OptionData>();
    public OptionData[] EffectTypes => Array.Empty<OptionData>();
    public OptionData[] GenderTypes => Array.Empty<OptionData>();
    public OptionData[] RewardTypes => Array.Empty<OptionData>();
    public OptionData[] ModificationTypes => Array.Empty<OptionData>();
    public OptionData[] ObjectiveTypes => Array.Empty<OptionData>();
    public OptionData[] EffectScalingType => Array.Empty<OptionData>();
    public OptionData[] StatTypes => Array.Empty<OptionData>();
    public OptionData[] StateTypes => Array.Empty<OptionData>();
    public OptionData[] CraftingSkillTypes => Array.Empty<OptionData>();
    public OptionData[] GatheringSkillTypes => Array.Empty<OptionData>();
    public OptionData[] TargetTypes => Array.Empty<OptionData>();
    public OptionData[] DamageTypes => Array.Empty<OptionData>();
    public OptionData[] ItemSlotTypes => Array.Empty<OptionData>();
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using OpenRpg.Editor.Core.Plugins;
using OpenRpg.Editor.Core.Services.Paths;

namespace OpenRpg.Editor.Infrastructure.Plugins;

public class ManifestPluginLoader
{
    private readonly List<(Assembly Assembly, EditorPluginInfo PluginInfo)> _loadedPlugins = new();
    private readonly List<string> _errors = new();
    private Assembly _defaultAssembly;

    public IReadOnlyList<EditorPluginInfo> LoadedPlugins => _loadedPlugins.Select(x => x.PluginInfo).ToList().AsReadOnly();
    public IReadOnlyList<string> Errors => _errors.AsReadOnly();
    public bool HasErrors => _errors.Count > 0;

    public void LoadPlugins()
    {
        _loadedPlugins.Clear();
        _errors.Clear();

        LoadDefaultManifest();
        ScanPluginsFolder();
    }

    private void LoadDefaultManifest()
    {
        var assembly = Assembly.GetExecutingAssembly();
        _defaultAssembly = assembly;

        var resourceName = "OpenRpg.Editor.Infrastructure.plugin.json";
        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            _errors.Add("Could not find default plugin.json embedded resource");
            return;
        }

        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();
        var manifest = JsonConvert.DeserializeObject<PluginManifest>(json);

        if (manifest == null || !manifest.IsValid())
        {
            _errors.Add("Default plugin manifest is invalid");
            return;
        }

        var typesProvider = new GenreTypesProvider(assembly, manifest);
        var pluginInfo = new EditorPluginInfo(manifest, typesProvider);
        _loadedPlugins.Add((assembly, pluginInfo));
    }

    private void ScanPluginsFolder()
    {
        var pluginPath = PathHelper.PluginPath;

        if (!Directory.Exists(pluginPath))
        {
            Directory.CreateDirectory(pluginPath);
            return;
        }

        var dllFiles = Directory.GetFiles(pluginPath, "OpenRpg.Genres*.dll", SearchOption.TopDirectoryOnly);

        foreach (var dllPath in dllFiles)
        {
            LoadPluginFromDll(dllPath);
        }
    }

    private void LoadPluginFromDll(string dllPath)
    {
        try
        {
            var assembly = Assembly.LoadFrom(dllPath);

            var manifest = LoadManifestFromAssembly(assembly, dllPath);
            if (manifest == null)
            {
                _errors.Add($"No valid manifest found in {Path.GetFileName(dllPath)}");
                return;
            }

            if (!manifest.IsValid())
            {
                _errors.Add($"Manifest in {Path.GetFileName(dllPath)} is invalid");
                return;
            }

            ValidateTypeSources(assembly, manifest, dllPath);

            var typesProvider = new GenreTypesProvider(assembly, manifest);
            var pluginInfo = new EditorPluginInfo(manifest, typesProvider);
            _loadedPlugins.Add((assembly, pluginInfo));
        }
        catch (Exception ex)
        {
            _errors.Add($"Failed to load plugin from {Path.GetFileName(dllPath)}: {ex.Message}");
        }
    }

    private PluginManifest? LoadManifestFromAssembly(Assembly assembly, string dllPath)
    {
        var resourceName = assembly.GetManifestResourceNames()
            .FirstOrDefault(n => n.EndsWith("plugin.json", StringComparison.OrdinalIgnoreCase));

        if (resourceName == null)
        {
            return null;
        }

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            return null;
        }

        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<PluginManifest>(json);
    }

    private void ValidateTypeSources(Assembly assembly, PluginManifest manifest, string dllPath)
    {
        foreach (var (key, typeSource) in manifest.TypeSources)
        {
            var parts = typeSource.Split(',');
            if (parts.Length != 2)
            {
                _errors.Add($"Invalid type source format for '{key}' in {Path.GetFileName(dllPath)}");
                continue;
            }

            var typeName = parts[0].Trim();
            var type = assembly.GetType(typeName);
            if (type == null)
            {
                _errors.Add($"Type '{typeName}' not found in {Path.GetFileName(dllPath)} for '{key}'");
            }
        }
    }

    public void UnloadPlugins()
    {
        _loadedPlugins.Clear();
        _errors.Clear();
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenRpg.Editor.Core.Plugins;
using OpenRpg.Editor.Core.Services.Paths;

namespace OpenRpg.Editor.Infrastructure.Plugins;

public class ManifestPluginLoader
{
    private readonly List<(Assembly Assembly, EditorPluginInfo PluginInfo)> _loadedPlugins = new();
    private readonly List<string> _errors = new();
    private Assembly _defaultAssembly;
    private readonly ILogger<ManifestPluginLoader> _logger;

    public ManifestPluginLoader(ILogger<ManifestPluginLoader> logger)
    {
        _logger = logger;
    }

    public IReadOnlyList<EditorPluginInfo> LoadedPlugins => _loadedPlugins.Select(x => x.PluginInfo).ToList().AsReadOnly();
    public IReadOnlyList<string> Errors => _errors.AsReadOnly();
    public bool HasErrors => _errors.Count > 0;

    public void LoadPlugins()
    {
        _loadedPlugins.Clear();
        _errors.Clear();

        _logger.LogInformation("Starting plugin load (default manifest + plugin folder scan)");
        LoadDefaultManifest();
        ScanPluginsFolder();
        _logger.LogInformation("Plugin loading complete: {PluginCount} plugins loaded, {ErrorCount} errors",
            _loadedPlugins.Count, _errors.Count);
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
            _logger.LogWarning("Default plugin.json embedded resource not found in {Assembly}", assembly.FullName);
            return;
        }

        using var reader = new StreamReader(stream);
        var json = reader.ReadToEnd();
        var manifest = JsonConvert.DeserializeObject<PluginManifest>(json);

        if (manifest == null || !manifest.IsValid())
        {
            _errors.Add("Default plugin manifest is invalid");
            _logger.LogWarning("Default plugin manifest is invalid or could not be deserialized");
            return;
        }

        var typesProvider = new GenreTypesProvider(assembly, manifest);
        var pluginInfo = new EditorPluginInfo(manifest, typesProvider);
        _loadedPlugins.Add((assembly, pluginInfo));

        _logger.LogInformation("Loaded default manifest: plugin '{PluginId}' with {TemplateTypeCount} template types, {TypeSourceCount} type sources",
            manifest.PluginId, manifest.TemplateTypes?.Count ?? 0, manifest.TypeSources?.Count ?? 0);
    }

    private void ScanPluginsFolder()
    {
        var pluginPath = PathHelper.PluginPath;

        if (!Directory.Exists(pluginPath))
        {
            _logger.LogInformation("Plugin folder '{PluginPath}' does not exist, creating it", pluginPath);
            Directory.CreateDirectory(pluginPath);
            return;
        }

        var dllFiles = Directory.GetFiles(pluginPath, "OpenRpg.Genres*.dll", SearchOption.TopDirectoryOnly);
        _logger.LogInformation("Found {DllCount} plugin DLL(s) in '{PluginPath}'", dllFiles.Length, pluginPath);

        foreach (var dllPath in dllFiles)
        {
            LoadPluginFromDll(dllPath);
        }
    }

    private void LoadPluginFromDll(string dllPath)
    {
        var dllName = Path.GetFileName(dllPath);
        try
        {
            _logger.LogInformation("Loading plugin DLL '{DllName}'", dllName);
            var assembly = Assembly.LoadFrom(dllPath);
            _logger.LogDebug("Loaded assembly '{AssemblyName}' from '{DllName}'", assembly.FullName, dllName);

            var manifest = LoadManifestFromAssembly(assembly, dllPath);
            if (manifest == null)
            {
                _errors.Add($"No valid manifest found in {dllName}");
                _logger.LogWarning("No plugin.json manifest found in assembly for '{DllName}'", dllName);
                return;
            }

            if (!manifest.IsValid())
            {
                _errors.Add($"Manifest in {dllName} is invalid");
                _logger.LogWarning("Plugin manifest in '{DllName}' is invalid", dllName);
                return;
            }

            ValidateTypeSources(assembly, manifest, dllPath);

            var typesProvider = new GenreTypesProvider(assembly, manifest);
            var pluginInfo = new EditorPluginInfo(manifest, typesProvider);
            _loadedPlugins.Add((assembly, pluginInfo));

            _logger.LogInformation("Successfully loaded plugin '{PluginId}' v{Version} from '{DllName}'",
                manifest.PluginId, manifest.SchemaVersion, dllName);
        }
        catch (Exception ex)
        {
            _errors.Add($"Failed to load plugin from {dllName}: {ex.Message}");
            _logger.LogError(ex, "Failed to load plugin from '{DllName}'", dllName);
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
                _logger.LogWarning("Invalid type source format for '{Key}' in '{DllName}': '{TypeSource}'",
                    key, Path.GetFileName(dllPath), typeSource);
                continue;
            }

            var typeName = parts[0].Trim();
            var type = assembly.GetType(typeName);
            if (type == null)
            {
                _errors.Add($"Type '{typeName}' not found in {Path.GetFileName(dllPath)} for '{key}'");
                _logger.LogWarning("Type '{TypeName}' not found in assembly from '{DllName}' for type source '{Key}'",
                    typeName, Path.GetFileName(dllPath), key);
            }
        }
    }

    public void UnloadPlugins()
    {
        _logger.LogInformation("Unloading all {PluginCount} plugins and clearing errors", _loadedPlugins.Count);
        _loadedPlugins.Clear();
        _errors.Clear();
    }
}
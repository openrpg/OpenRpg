using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Editor.Core.Genres;
using OpenRpg.Editor.Core.Services.Paths;
using OpenRpg.Editor.Core.Services.Notifications;
using OpenRpg.Editor.Core.Events;

namespace OpenRpg.Editor.Infrastructure.Plugins;

public class GenrePluginLoader
{
    private readonly INotifier _notifier;
    private readonly List<IGenrePlugin> _loadedPlugins = new();
    private readonly List<Assembly> _loadedAssemblies = new();

    public IReadOnlyList<IGenrePlugin> LoadedPlugins => _loadedPlugins.AsReadOnly();

    public GenrePluginLoader(INotifier notifier)
    {
        _notifier = notifier;
    }

    public void LoadPlugins()
    {
        _loadedPlugins.Clear();
        var pluginPath = PathHelper.PluginPath;

        if (!Directory.Exists(pluginPath))
        {
            Directory.CreateDirectory(pluginPath);
            return;
        }

        var dllFiles = Directory.GetFiles(pluginPath, "OpenRpg.Genres*.dll", SearchOption.TopDirectoryOnly);

        foreach (var dllPath in dllFiles)
        {
            LoadPluginFromDllSync(dllPath);
        }
    }

    public void LoadPluginsAsync()
    {
        LoadPlugins();
    }

    private void LoadPluginFromDllSync(string dllPath)
    {
        try
        {
            var assemblyName = AssemblyName.GetAssemblyName(dllPath);
            var assembly = Assembly.Load(assemblyName);
            _loadedAssemblies.Add(assembly);

            var pluginType = assembly.GetTypes()
                .FirstOrDefault(t => typeof(IGenrePlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            if (pluginType == null)
            {
                return;
            }

            var plugin = (IGenrePlugin)Activator.CreateInstance(pluginType);
            plugin.Initialize(new ServiceCollection());
            _loadedPlugins.Add(plugin);
        }
        catch (Exception)
        {
        }
    }

    private async Task LoadPluginFromDll(string dllPath)
    {
        try
        {
            var assemblyName = AssemblyName.GetAssemblyName(dllPath);
            var assembly = Assembly.Load(assemblyName);

            var pluginType = assembly.GetTypes()
                .FirstOrDefault(t => typeof(IGenrePlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            if (pluginType == null)
            {
                return;
            }

            var plugin = (IGenrePlugin)Activator.CreateInstance(pluginType);
            plugin.Initialize(new ServiceCollection());
            _loadedPlugins.Add(plugin);
        }
        catch (Exception)
        {
        }
    }

    public void UnloadPlugins()
    {
        _loadedAssemblies.Clear();
        _loadedPlugins.Clear();
    }
}

public class GenrePluginMetadata
{
    private readonly IGenrePlugin _plugin;

    public GenrePluginMetadata(IGenrePlugin plugin)
    {
        _plugin = plugin;
    }

    public string Id => _plugin.Id;
    public string Name => _plugin.Name;
    public string Version => _plugin.Version;
    public string Description => _plugin.Description;
}

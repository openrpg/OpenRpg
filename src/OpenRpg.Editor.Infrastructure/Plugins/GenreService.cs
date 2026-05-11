using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Editor.Core.Genres;

namespace OpenRpg.Editor.Infrastructure.Plugins;

public class GenreService
{
    private readonly GenrePluginLoader _pluginLoader;
    private readonly List<IGenrePlugin> _enabledGenres = new();
    private bool _initialized = false;

    public IReadOnlyList<IGenrePlugin> EnabledGenres => _enabledGenres.AsReadOnly();
    public IReadOnlyList<IGenrePlugin> AvailablePlugins => _pluginLoader.LoadedPlugins;
    public IEnumerable<GenrePluginMetadata> AvailableGenres => _pluginLoader.LoadedPlugins.Select(p => new GenrePluginMetadata(p));
    public bool HasGenresLoaded => _pluginLoader.LoadedPlugins.Count > 0;
    public bool HasEnabledGenres => _enabledGenres.Count > 0;

    public GenreService(GenrePluginLoader pluginLoader)
    {
        _pluginLoader = pluginLoader;
    }

    private void EnsureInitialized()
    {
        if (_initialized) return;
        _initialized = true;
        
        _pluginLoader.LoadPlugins();
        
        if (_pluginLoader.LoadedPlugins.Count == 0)
        {
            var defaultPlugin = new DefaultGenrePlugin();
            _enabledGenres.Add(defaultPlugin);
        }
    }

    public void RefreshPlugins()
    {
        _pluginLoader.LoadPlugins();
    }

    public void EnableGenre(IGenrePlugin plugin)
    {
        EnsureInitialized();
        if (_enabledGenres.Any(g => g.Id == plugin.Id))
            return;

        _enabledGenres.Add(plugin);
    }

    public bool EnableGenre(string genreId)
    {
        EnsureInitialized();
        var plugin = _pluginLoader.LoadedPlugins.FirstOrDefault(p => p.Id == genreId);
        if (plugin == null) return false;
        
        if (_enabledGenres.Any(g => g.Id == plugin.Id))
            return true;

        _enabledGenres.Add(plugin);
        return true;
    }

    public void DisableGenre(string genreId)
    {
        EnsureInitialized();
        _enabledGenres.RemoveAll(g => g.Id == genreId);
    }

    public void SetEnabledGenres(IEnumerable<IGenrePlugin> genres)
    {
        EnsureInitialized();
        _enabledGenres.Clear();
        _enabledGenres.AddRange(genres);
    }

    public IGenreTypesProvider GetCombinedTypesProvider()
    {
        EnsureInitialized();
        if (_enabledGenres.Count == 0)
        {
            return new DefaultGenreTypesProvider();
        }
        return new CombinedGenreTypesProvider(_enabledGenres);
    }

    public IGenreRuntimeServices GetCombinedRuntimeServices()
    {
        EnsureInitialized();
        return new CombinedGenreRuntimeServices(_enabledGenres);
    }

    public (bool IsValid, string ErrorMessage) ValidateGenreCombination(IGenrePlugin newGenre)
    {
        EnsureInitialized();
        var existingIds = _enabledGenres
            .SelectMany(g => g.TypesProvider.ItemTypes.Select(t => t.Id))
            .ToHashSet();

        var newIds = newGenre.TypesProvider.ItemTypes.Select(t => t.Id);

        foreach (var id in newIds)
        {
            if (existingIds.Contains(id))
            {
                var existingGenre = _enabledGenres.FirstOrDefault(g =>
                    g.TypesProvider.ItemTypes.Any(t => t.Id == id));
                return (false, $"Type ID {id} conflict between {existingGenre?.Name} and {newGenre.Name}");
            }
        }

        return (true, string.Empty);
    }

    public void RegisterGenreServices(IServiceCollection services)
    {
        EnsureInitialized();
        foreach (var genre in _enabledGenres)
        {
            genre.RuntimeServices.RegisterServices(services);
        }
    }
}

public class CombinedGenreTypesProvider : IGenreTypesProvider
{
    public string GenreId => "combined";
    public string GenreName => "Combined";

    private readonly List<IGenreTypesProvider> _providers;

    public CombinedGenreTypesProvider(IEnumerable<IGenrePlugin> plugins)
    {
        _providers = plugins.Select(p => p.TypesProvider).ToList();
    }

    public OptionData[] ItemTypes => _providers.SelectMany(p => p.ItemTypes).ToArray();
    public OptionData[] ItemQualityTypes => _providers.SelectMany(p => p.ItemQualityTypes).ToArray();
    public OptionData[] RequirementTypes => _providers.SelectMany(p => p.RequirementTypes).ToArray();
    public OptionData[] EffectTypes => _providers.SelectMany(p => p.EffectTypes).ToArray();
    public OptionData[] RewardTypes => _providers.SelectMany(p => p.RewardTypes).ToArray();
    public OptionData[] ModificationTypes => _providers.SelectMany(p => p.ModificationTypes).ToArray();
    public OptionData[] ObjectiveTypes => _providers.SelectMany(p => p.ObjectiveTypes).ToArray();
    public OptionData[] EffectScalingType => _providers.SelectMany(p => p.EffectScalingType).ToArray();
    public OptionData[] StatTypes => _providers.SelectMany(p => p.StatTypes).ToArray();
    public OptionData[] StateTypes => _providers.SelectMany(p => p.StateTypes).ToArray();
    public OptionData[] CraftingSkillTypes => _providers.SelectMany(p => p.CraftingSkillTypes).ToArray();
    public OptionData[] GatheringSkillTypes => _providers.SelectMany(p => p.GatheringSkillTypes).ToArray();
}

public class CombinedGenreRuntimeServices : IGenreRuntimeServices
{
    private readonly List<IGenrePlugin> _plugins;

    public CombinedGenreRuntimeServices(IEnumerable<IGenrePlugin> plugins)
    {
        _plugins = plugins.ToList();
    }

    public void RegisterServices(IServiceCollection services)
    {
        foreach (var plugin in _plugins)
        {
            plugin.RuntimeServices.RegisterServices(services);
        }
    }
}
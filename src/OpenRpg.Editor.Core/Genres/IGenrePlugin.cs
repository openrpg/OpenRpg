using Microsoft.Extensions.DependencyInjection;

namespace OpenRpg.Editor.Core.Genres;

public interface IGenrePlugin
{
    string Id { get; }
    string Name { get; }
    string Version { get; }
    string Description { get; }

    IGenreTypesProvider TypesProvider { get; }
    IGenreRuntimeServices RuntimeServices { get; }

    void Initialize(IServiceCollection services);
}

using Microsoft.Extensions.DependencyInjection;

namespace OpenRpg.Editor.Core.Genres;

public interface IGenreRuntimeServices
{
    void RegisterServices(IServiceCollection services);
}

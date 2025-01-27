using Microsoft.Extensions.DependencyInjection;
using OpenRpg.Demos.Infrastructure.DI;

namespace OpenRpg.Demos.Web.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddModule(this IServiceCollection services, IModule module)
        {
            module.Setup(services);
            return services;
        }
        
        public static IServiceCollection AddModule<T>(this IServiceCollection services) where T : IModule, new()
        { return services.AddModule(new T()); }
    }
}
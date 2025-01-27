using Microsoft.Extensions.DependencyInjection;

namespace OpenRpg.Demos.Infrastructure.DI
{
    public interface IModule
    {
        void Setup(IServiceCollection services);
    }
}
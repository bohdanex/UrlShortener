using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Services;
using UrlShortener.Services.Abstraction;

namespace UrlShortenerMVC
{
    public static class ServiceInjector
    {
        public static void InjectAllServices(this IServiceCollection services)
        {
            services.AddSingleton<IUrlHasher, ASCIIUrlHasher>();
            services.AddSingleton<IUserSe>();
        }
    }
}

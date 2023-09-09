using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Services;
using UrlShortener.Services.Abstraction;
using UrlShortener.Services.Implementation;
using UrlShortenerDataAccess.Repositories;
using UrlShortenerDataAccess.Repositories.Abstraction;

namespace UrlShortenerMVC
{
    public static class ServiceInjector
    {
        public static void InjectAllServices(this IServiceCollection services)
        {
            //Singleton services
            services.AddSingleton<IUrlHasherService, SHA1Hasher>();
            services.AddSingleton<ISecurityService, SecurityService>();

            //Scoped services
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddScoped<IBaseUrlService, BaseUrlService>();
        }
    }
}

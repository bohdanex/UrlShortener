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
            services.AddSingleton<IUrlHasherService, SHA1Hasher>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<ISecurityService, SecurityService>();
        }
    }
}

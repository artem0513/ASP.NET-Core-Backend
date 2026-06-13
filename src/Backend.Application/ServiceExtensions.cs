using Backend.Application.Interfaces;
using Backend.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Application
{
    public static class ServiceExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
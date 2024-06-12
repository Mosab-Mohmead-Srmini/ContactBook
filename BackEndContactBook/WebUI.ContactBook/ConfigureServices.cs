using ContactBook.Application.Common.Interfaces;
using Infrastructure.Services;
using Microsoft.OpenApi.Models;

namespace WebUI.ContactBook
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebUIServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register ICurrentUserService
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.AddLogging();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}

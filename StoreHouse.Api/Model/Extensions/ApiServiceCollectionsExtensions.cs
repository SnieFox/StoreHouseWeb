using StoreHouse.Api.Services;
using StoreHouse.Api.Services.Interfaces;

namespace StoreHouse.Api.Model.Extensions;

public static class ApiServiceCollectionsExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        
        services.AddScoped<IStatisticsService, StatisticsService>();
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<IMenuService, MenuService>();

        return services;
    }
}
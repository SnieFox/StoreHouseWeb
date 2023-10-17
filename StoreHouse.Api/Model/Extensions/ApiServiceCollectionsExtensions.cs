using StoreHouse.Api.Services.StatisticsServices;
using StoreHouse.Api.Services.StatisticsServices.StatisticsInterfaces;

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

        return services;
    }
}
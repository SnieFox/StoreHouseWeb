using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreHouse.Database.Services;
using StoreHouse.Database.Services.Interfaces;
using StoreHouse.Database.StoreHouseDbContext;

namespace StoreHouse.Database.Extensions;

public static class DbServiceCollectionsExtensions
{
    public static IServiceCollection AddDbServices(this IServiceCollection services, string connectionString)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Connection string must be not null or empty", nameof(connectionString));
        }
        
        services.AddDbContext<StoreHouseContext>(db =>
            db.UseNpgsql(connectionString,
                b=> b.MigrationsAssembly("StoreHouse.Api")));

        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IDishService, DishService>();
        services.AddScoped<IIngredientCategoryService, IngredientCategoryService>();
        services.AddScoped<IIngredientService, IngredientService>();
        services.AddScoped<IProductCategoryService, ProductCategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IReceiptService, ReceiptService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ISemiProductService, SemiProductService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<ISupplyService, SupplyService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWriteOffCauseService, WriteOffCauseService>();
        services.AddScoped<IWriteOffService, WriteOffService>();
        services.AddScoped<IOrganizationService, OrganizationService>();

        return services;
    }
    
}
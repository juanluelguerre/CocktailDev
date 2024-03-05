using CocktailDev.Products.Api.Domain.Aggregates;
using CocktailDev.Products.Api.Infrastructure.EntityFramework;
using CocktailDev.Products.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CocktailDev.Products.Api.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddProducts(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddDbContext(configuration);
        services.AddRepositories();
        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        // 1) SqlServer
        //services.AddDbContext<ProductContext>(options =>
        //{
        //    options.UseSqlServer(
        //        configuration.GetConnectionString(ProductContext.ConnectionString),
        //        ConfigureSqlOptions);
        //});

        // 2) InMemory
        services.AddDbContext<ProductContext>(options =>
        {
            options.UseInMemoryDatabase("Products");
        });


        return services;

        static void ConfigureSqlOptions(SqlServerDbContextOptionsBuilder sqlOptions)
        {
            sqlOptions.MigrationsAssembly(typeof(InfrastructureExtensions).Assembly.FullName);

            // Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 

            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15,
                maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        }
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}

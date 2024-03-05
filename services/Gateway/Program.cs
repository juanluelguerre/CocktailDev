using CocktailDev.Gateway;
using CocktailDev.Gateway.Application.Builders;
using CocktailDev.Gateway.Application.Queries;
using CocktailDev.Gateway.Domain;
using CocktailDev.Gateway.Infrastructure.Repositories;
using HotChocolate.AspNetCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
//builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();

builder.Services.AddHttpClient<IProductRepository, ProductRepository>(client =>
{
    //TODO configure url at appSetting.json file
    client.BaseAddress = new Uri("https://localhost:6001");
});

builder.Services.AddHttpClient<IOrderRepository, OrderRepository>(client =>
{
    //TODO configure url at appSetting.json file
    client.BaseAddress = new Uri("https://localhost:7001");
});

builder.Services.AddScoped<IOrderDetailsBuilder, OrderDetailsBuilder>();

builder.Services
    .AddGraphQLServer()
    .AddCacheControl()
    .AddQueryType(q => q.Name("Query"))
    .AddType<ProductsQuery>()
    .AddType<OrdersQuery>()
    .AddType<OrderSummaryQuery>();
// .UseAutomaticPersistedQueryPipeline();
// .AddInMemoryQueryStorage();

builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
        tracerProviderBuilder
            .AddSource(DiagnosticsConfig.ActivitySource.Name)
            .ConfigureResource(resource => resource
                .AddService(DiagnosticsConfig.ServiceName))
            .AddAspNetCoreInstrumentation()
            .AddConsoleExporter());

var app = builder.Build();

app.MapGraphQL().WithOptions(new GraphQLServerOptions
{
    Tool =
    {
        Enable = app.Environment.IsDevelopment()
    }
});

app.Run();

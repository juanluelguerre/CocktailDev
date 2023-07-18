// Ref: https://github.com/graphql-dotnet/examples/blob/master/src/AspNetCoreController/Example/Startup.cs

using CocktailDev.Gateway;
using CocktailDev.Gateway.Application.Resolvers;
using CocktailDev.Gateway.Domain;
using CocktailDev.Gateway.Infrastructure.Repositories;
using CocktailDev.Gateway.Schemas;
using GraphQL;
using GraphQL.Server.Ui.Playground;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<OrderSummarySchema>();
builder.Services.AddGraphQL(b => b
    .AddSchema<OrderSummarySchema>()
    .AddSystemTextJson()
    // .AddValidationRule<InputValidationRule>()
    .AddGraphTypes(typeof(Program).Assembly)
    //.AddMemoryCache()
    .AddApolloTracing(options => options.RequestServices!
        .GetRequiredService<IOptions<GraphQLSettings>>().Value.EnableMetrics));

// builder.Services.Configure<GraphQLSettings>(Configuration.GetSection("GraphQLSettings"));
// builder.Services.Configure<PlaygroundOptions>();

// builder.Services.AddProductApiServices(); // Configure Product API rest
// builder.Services.AddOrderApiServices(); // Configure Order API rest

builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();

builder.Services.AddScoped<IProductQueryResolver, ProductQueryResolver>();
builder.Services.AddScoped<IOrderQueryResolver, OrderQueryResolver>();


builder.Services.AddControllers();

var app = builder.Build();

//app.UseGraphQL<OrderSummarySchema>();
//app.UseGraphQLPlayground(new PlaygroundOptions());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

// app.UseEndpoints(endpoints => { endpoints.MapGraphQL(); });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

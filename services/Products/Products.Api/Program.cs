using CocktailDev.Products.Api;
using CocktailDev.Products.Api.Domain;
using CocktailDev.Products.Api.Infrastructure.Repositories;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Formatting.Elasticsearch;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;

var builder = WebApplication.CreateBuilder(args);
//TODO: Demo (1) Adding Serilog
builder.Logging.ClearProviders();

builder.Host.UseSerilog((context, services, config) =>
{
    config
        .MinimumLevel.Information()
        .Enrich.WithMachineName()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(
            new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticSearch:Uri"] ??
                                                 "http://localhost:9200"))
            {
                IndexFormat =
                    $"{context.Configuration["ApplicationName"]?.ToLower().Replace(".", "-")}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyyMM}",
                AutoRegisterTemplate = true,
                DetectElasticsearchVersion = true,
                ModifyConnectionSettings = x =>
                    x.BasicAuthentication(context.Configuration["ElasticSearch:User"],
                        context.Configuration["ElasticSearch:Password"]),
                FailureCallback = (ex) =>
                    Console.WriteLine("Unable to submit event " + ex.MessageTemplate),
                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                   EmitEventFailureHandling.WriteToFailureSink |
                                   EmitEventFailureHandling.RaiseCallback,
                FailureSink = new FileSink($"./fail-{DateTime.UtcNow:yyyyMM}.txt",
                    new JsonFormatter(), null, null)
            }).Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
        .ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    // cfg.AddBehavior<...abc..>();
});

builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddOpenTelemetry()
//    .WithTracing(tracerProviderBuilder =>
//        tracerProviderBuilder
//            .AddSource(DiagnosticsConfig.ActivitySource.Name)
//            .ConfigureResource(resource => resource
//                .AddService(DiagnosticsConfig.ServiceName))
//            .AddAspNetCoreInstrumentation()
//            .AddConsoleExporter());


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

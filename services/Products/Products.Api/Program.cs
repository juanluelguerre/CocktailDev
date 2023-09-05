﻿using System.Text;
using CocktailDev.Products.Api;
using CocktailDev.Products.Api.Domain;
using CocktailDev.Products.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
//TODO: Demo (1) Adding Serilog
builder.Logging.ClearProviders();


builder.Host.UseSerilog((context, services, config) =>
{
    // Important: Debug Serilog Sinks errors
    Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

    config
        .MinimumLevel.Information()
        .Enrich.WithMachineName()
        .WriteTo.Console()
        //.WriteTo.Elasticsearch(
        //    new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticSearch:Uri"] ??
        //                                         "http://localhost:9200"))
        //    {
        //        IndexFormat =
        //            $"{context.Configuration["ApplicationName"]?.ToLower().Replace(".", "-")}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyyMM}",
        //        AutoRegisterTemplate = true,
        //        ModifyConnectionSettings = c => c
        //            .ConnectionLimit(-1)
        //            .BasicAuthentication(context.Configuration["ElasticSearch:User"],
        //                context.Configuration["ElasticSearch:Password"])
        //            // TODO: Do not use this at Production environment.
        //            .ServerCertificateValidationCallback((obj, certificate, chain, errors) => true),
        //        FailureCallback = (ex) =>
        //            Console.WriteLine("Unable to submit event " + ex.MessageTemplate),
        //        EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
        //                           EmitEventFailureHandling.WriteToFailureSink |
        //                           EmitEventFailureHandling.RaiseCallback,
        //    }).Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
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

// Ref: https://www.elastic.co/blog/manual-instrumentation-of-net-applications-opentelemetry
builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
        tracerProviderBuilder
            .AddSource(DiagnosticsConfig.ActivitySource.Name)
            .ConfigureResource(resource => resource
                .AddService(DiagnosticsConfig.ServiceName))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter()
            .AddElasticsearchClientInstrumentation(options => { })
            .AddOtlpExporter(options =>
            {
                options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
                options.ExportProcessorType = ExportProcessorType.Simple;
                options.Endpoint = new Uri("http://localhost:8200");
                options.Headers = "Authorization=Bearer xxxxxx";
                //options.HttpClientFactory = () =>
                //{
                //    var handler = new HttpClientHandler
                //    {
                //        ServerCertificateCustomValidationCallback =
                //            (sender, cert, chain, sslPolicyErrors) => true
                //    };

                //    var client = new HttpClient(handler)
                //    {
                //        BaseAddress = new Uri("https://localhost:9200")
                //    };

                //    //const string authenticationString = $"elastic:hx21ixWh2W4OdJWs";
                //    //var base64EncodedAuthenticationString =
                //    //    Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));
                //    //client.DefaultRequestHeaders.Add("Authorization",
                //    //    "Basic " + base64EncodedAuthenticationString);
                //    return client;
                //};
            })
    );


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

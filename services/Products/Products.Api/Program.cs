using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using CocktailDev.Products.Api;
using CocktailDev.Products.Api.Domain;
using CocktailDev.Products.Api.Infrastructure.Repositories;
using Elastic.Apm.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Exporter;
using Serilog;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Hosting.Server;
using Serilog.Sinks.OpenTelemetry;
using Serilog.Core;
using OpenTelemetry.Metrics;

var builder = WebApplication.CreateBuilder(args);

//TODO: Demo (1) Adding Serilog

#region Serilog

//// builder.Logging.ClearProviders();

////builder.Host.UseSerilog((context, services, config) =>
////{
////    // Important: Debug Serilog Sinks errors
////    Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));
////    Serilog.Debugging.SelfLog.Enable(Console.Error);

////    config
////        .MinimumLevel.Information()
////        .Enrich.WithMachineName()
////        .WriteTo.Console()
////        //.WriteTo.OpenTelemetry(options =>
////        //{
////        //    options.Endpoint = "http://localhost:4318/v1/logs";
////        //    options.Protocol = OtlpProtocol.HttpProtobuf;
////        //})

////        //endpoint: "http://localhost:4318",
////        // protocol: OtlpProtocol.HttpProtobuf)

////        //.WriteTo.Elasticsearch(
////        //    new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticSearch:Uri"] ??
////        //                                         "http://localhost:9200"))
////        //    {
////        //        IndexFormat =
////        //            $"{context.Configuration["ApplicationName"]?.ToLower().Replace(".", "-")}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyyMM}",
////        //        AutoRegisterTemplate = true,
////        //        ModifyConnectionSettings = c => c
////        //            .ConnectionLimit(-1)
////        //            .BasicAuthentication(context.Configuration["ElasticSearch:User"],
////        //                context.Configuration["ElasticSearch:Password"])
////        //            // TODO: Do not use this at Production environment.
////        //            .ServerCertificateValidationCallback((obj, certificate, chain, errors) => true),
////        //        FailureCallback = (ex) =>
////        //            Console.WriteLine("Unable to submit event " + ex.MessageTemplate),
////        //        EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
////        //                           EmitEventFailureHandling.WriteToFailureSink |
////        //                           EmitEventFailureHandling.RaiseCallback,
////        //    }).Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
////        .ReadFrom.Configuration(context.Configuration);


////    // Ref: https://dev.to/kim-ch/observability-net-opentelemetry-collector-25g1
////    // Only export to OpenTelemetry collector    
////    //config.WriteTo.OpenTelemetry(cfg =>
////    //{
////    //    cfg.Endpoint = $"https://localhost:8200/v1/logs";
////    //    cfg.IncludedData = IncludedData.TraceIdField | IncludedData.SpanIdField;
////    //    cfg.ResourceAttributes = new Dictionary<string, object>
////    //    {
////    //        {"service.name", observabilityOptions.ServiceName},
////    //        {"index", 10},
////    //        {"flag", true},
////    //        {"value", 3.14}
////    //    };
////});

#endregion

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    // cfg.AddBehavior<...abc..>();
});

builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Logging.AddOpenTelemetry(logs => logs
    //.AddConsoleExporter()
    .AddOtlpExporter(options =>
    {
        options.Endpoint = new Uri("http://localhost:4318/v1/logs");
        options.Protocol = OtlpExportProtocol.HttpProtobuf;
    }));

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(serviceName: builder.Environment.ApplicationName,
            serviceInstanceId: Environment.MachineName)
        .AddAttributes(new Dictionary<string, object>
        {
            //    ["service.name"] = builder.Environment.ApplicationName.ToLowerInvariant(),
            //    ["service.node.name"] = Environment.MachineName.ToLowerInvariant(),
            //    ["service.environment"] = builder.Environment.EnvironmentName.ToLowerInvariant(),
            ["host.name"] = Environment.MachineName,
            ["os.description"] = RuntimeInformation.OSDescription,
            ["deployment.environment"] =
                builder.Environment.EnvironmentName.ToLowerInvariant(),
        })
    )
    .WithTracing(tracing => tracing.SetErrorStatusOnException()
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        // .AddHangfireInstrumentation()
        // .AddRedisInstrumentation(connection)
        // .AddElasticsearchClientInstrumentation()
        // .AddConsoleExporter()
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri("http://localhost:4318/v1/traces");
            options.Protocol = OtlpExportProtocol.HttpProtobuf;
        }))
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddRuntimeInstrumentation()
        .AddProcessInstrumentation()
        .AddHttpClientInstrumentation()
        .AddPrometheusExporter()
        // .AddHangfireInstrumentation()
        // .AddRedisInstrumentation(connection)
        // .AddElasticsearchClientInstrumentation()
        .AddMeter("Microsoft.AspNetCore.Hosting")
        .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri("http://localhost:4318/v1/metrics");
            options.Protocol = OtlpExportProtocol.HttpProtobuf;
        }));
;


// Ref: https://www.elastic.co/blog/manual-instrumentation-of-net-applications-opentelemetry
//builder.Services.AddOpenTelemetry()
//    // .WithMetrics()
//    .WithTracing(config =>
//        config.SetErrorStatusOnException(false)
//            .AddSource(DiagnosticsConfig.ActivitySource.Name)
//            .ConfigureResource(resource => resource
//                .AddService(DiagnosticsConfig.ServiceName))
//            .AddAspNetCoreInstrumentation()
//            .AddHttpClientInstrumentation()
//            // .AddConsoleExporter()
//            // .AddElasticsearchClientInstrumentation()            
//            .AddOtlpExporter(options =>
//            {
//                // options.Protocol = OtlpExportProtocol.HttpProtobuf;
//                //options.ExportProcessorType = ExportProcessorType.Simple;
//                options.Endpoint = new Uri("http://localhost:4317");

//                // options.Headers = "Authorization=Bearer k9igmeF267v663TW51BlmE2V";
//                //options.HttpClientFactory = () =>
//                //{
//                //    var handler = new HttpClientHandler
//                //    {
//                //        ServerCertificateCustomValidationCallback =
//                //            (sender, cert, chain, sslPolicyErrors) => true
//                //    };

//                //    var client = new HttpClient(handler)
//                //    {
//                //        BaseAddress = new Uri("http://localhost:4317")
//                //    };

//                //    //const string authenticationString = $"elastic:hx21ixWh2W4OdJWs";
//                //    //var base64EncodedAuthenticationString =
//                //    //    Convert.ToBase64String(Encoding.UTF8.GetBytes(authenticationString));
//                //    //client.DefaultRequestHeaders.Add("Authorization",
//                //    //    "Basic " + base64EncodedAuthenticationString);
//                //    return client;
//                //};
//            }));

// Register a Tracer, so it can be injected into other components (for example, Controllers)
// builder.Services.AddSingleton(TracerProvider.Default.GetTracer(DiagnosticsConfig.ServiceName));

var app = builder.Build();

//app.MapGet("/", (Tracer tracer) =>
//{
//    using var span = tracer.StartActiveSpan("app.manual-span");
//    span.SetAttribute("app.manual-span.message", "Adding custom spans is also super easy!");
//});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// app.UseElasticApm(app.Configuration);

app.MapPrometheusScrapingEndpoint();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

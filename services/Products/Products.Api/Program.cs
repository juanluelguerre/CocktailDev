using System.Net.NetworkInformation;
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


    // Ref: https://dev.to/kim-ch/observability-net-opentelemetry-collector-25g1
    // Only export to OpenTelemetry collector    
    //config.WriteTo.OpenTelemetry(cfg =>
    //{
    //    cfg.Endpoint = $"https://localhost:8200/v1/logs";
    //    cfg.IncludedData = IncludedData.TraceIdField | IncludedData.SpanIdField;
    //    cfg.ResourceAttributes = new Dictionary<string, object>
    //    {
    //        {"service.name", observabilityOptions.ServiceName},
    //        {"index", 10},
    //        {"flag", true},
    //        {"value", 3.14}
    //    };
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

builder.Services.AddOpenTelemetry()
    .WithTracing(config =>
        config.SetErrorStatusOnException()
            .AddSource(DiagnosticsConfig.ActivitySource.Name)
            .ConfigureResource(resource => resource
                .AddService(DiagnosticsConfig.ServiceName))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
// .AddConsoleExporter()
//.AddZipkinExporter(o => o.HttpClientFactory = () =>
//{
//    var client = new HttpClient();
//    client.DefaultRequestHeaders.Add("X-MyCustomHeader", "value");
//    return client;
//}));
            .AddOtlpExporter(options =>
            {
                //options.Endpoint = new Uri("http://localhost:4317");
                options.Endpoint =
                    new Uri("http://opentelemetry-collector.elk.svc:4317");
                options.Protocol = OtlpExportProtocol.HttpProtobuf;
                options.Headers = "Authorization=Bearer k9igmeF267v663TW51BlmE2V";
                // options.ExportProcessorType = ExportProcessorType.Batch;
                //options.HttpClientFactory = () =>
                //{
                //    var client = new HttpClient();
                //    client.DefaultRequestHeaders.Add("X-MyCustomHeader", "value");
                //    return client;
                //};
            }));


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


Environment.SetEnvironmentVariable("OTEL_LOG_LEVEL", "1");
Environment.SetEnvironmentVariable("COREHOST_TRACEFILE", "corehost_verbose_tracing.log");

Environment.SetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT", "https://localhost:8200");
Environment.SetEnvironmentVariable("OTEL_EXPORTER_OTLP_HEADERS",
    "Authorization=Bearer k9igmeF267v663TW51BlmE2V");
Environment.SetEnvironmentVariable("OTEL_METRICS_EXPORTER", "otlp");
Environment.SetEnvironmentVariable("OTEL_LOGS_EXPORTER", "otlp");
Environment.SetEnvironmentVariable("OTEL_RESOURCE_ATTRIBUTES",
    "service.name=cocktaildev,service.version=1.0.0,deployment.environment=development");


// OTEL_EXPORTER_OTLP_ENDPOINT: // https://localhost:8200 // https://apm-server-apm-http.default.svc:8200 
// OTEL_EXPORTER_OTLP_HEADERS: "Authorization=Bearer <secret-token>"
// OTEL_METRICS_EXPORTER: otlp
// OTEL_LOGS_EXPORTER: otlp
// OTEL_RESOURCE_ATTRIBUTES: service.name=<app-name>,service.version=<app-version>,deployment.environment=production

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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

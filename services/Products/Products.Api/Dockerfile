#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy the generated developer certificate into the container
COPY certs/* /https/

# Set the ASP.NET Core environment variable ASPNETCORE_URLS for HTTPS
ENV ASPNETCORE_URLS=https://+:443

# Set the environment variable ASPNETCORE_Kestrel__Certificates__Default__Password for the certificate password (can be a fixed password)
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=password1

# Set the environment variable ASPNETCORE_Kestrel__Certificates__Default__Path for the certificate location
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

ENV OTEL_EXPORTER_OTLP_TRACES_ENDPOINT=opentelemetry-collector.elk.svc:4317
ENV OTEL_EXPORTER_OTLP_ENDPOINT=https://apm-server-apm-http.elk.svc:8200
ENV OTEL_EXPORTER_OTLP_HEADERS="Authorization=Bearer k9igmeF267v663TW51BlmE2V"
ENV OTEL_METRICS_EXPORTER=otlp
ENV OTEL_LOGS_EXPORTER=otlp
ENV OTEL_RESOURCE_ATTRIBUTES=service.name="cocktaildev.products.api,service.version=1,deployment.environment=development"

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CocktailDev.Products.Api.csproj", "Products.Api/"]
RUN dotnet restore "Products.Api/CocktailDev.Products.Api.csproj"
COPY . Products.Api/
WORKDIR "Products.Api"
RUN dotnet build "CocktailDev.Products.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CocktailDev.Products.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CocktailDev.Products.Api.dll"]
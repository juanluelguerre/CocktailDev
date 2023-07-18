FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/Gateway/CocktailDev.Gateway.csproj", "services/Gateway/"]
RUN dotnet restore "services/Gateway/GCocktailDev.ateway.csproj"
COPY . .
WORKDIR "/src/services/Gateway"
RUN dotnet build "CocktailDev.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CocktailDev.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CocktailDev.Gateway.dll"]
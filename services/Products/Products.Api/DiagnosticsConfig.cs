using System.Diagnostics;

namespace CocktailDev.Products.Api;

public static class DiagnosticsConfig
{
    public static string ServiceName = "CocktailDev.Products.Api";
    public static ActivitySource ActivitySource = new(ServiceName);
}

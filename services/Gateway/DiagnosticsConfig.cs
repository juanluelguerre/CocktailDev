using System.Diagnostics;

namespace CocktailDev.Gateway;

public static class DiagnosticsConfig
{
    public static string ServiceName = "CocktailDev.Gateway";
    public static ActivitySource ActivitySource = new(ServiceName);
}

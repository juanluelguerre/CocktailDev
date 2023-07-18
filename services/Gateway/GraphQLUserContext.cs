using System.Security.Claims;

namespace CocktailDev.Gateway;

public class GraphQLUserContext : Dictionary<string, object>
{
    public ClaimsPrincipal User { get; set; }
}

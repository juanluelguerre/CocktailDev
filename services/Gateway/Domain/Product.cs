using System.Text.Json.Serialization;

namespace CocktailDev.Gateway.Domain;

public record Product(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("name")] string Name);

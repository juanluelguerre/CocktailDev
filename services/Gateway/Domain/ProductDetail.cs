using System.Text.Json.Serialization;

namespace CocktailDev.Gateway.Domain;

public record ProductDetail(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("price")] decimal Price);

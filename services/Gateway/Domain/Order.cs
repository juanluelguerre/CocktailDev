using System.Text.Json.Serialization;

namespace CocktailDev.Gateway.Domain;

public record Order(
    [property: JsonPropertyName("customerName")]
    string CustomerName,
    [property: JsonPropertyName("products")]
    List<Product> Products);

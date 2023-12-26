namespace CocktailDev.Products.Api.Domain;

public readonly record struct ProductDetail(
    long Id,
    string Name,
    string Description,
    decimal Price);

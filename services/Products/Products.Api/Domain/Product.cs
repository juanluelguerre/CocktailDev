namespace CocktailDev.Products.Api.Domain;

public readonly record struct Product(long Id, string Name, decimal Price);

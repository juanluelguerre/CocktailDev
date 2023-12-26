namespace CocktailDev.Products.Api.Application.Requests;

public record ProductRequest(long Id, string Name, string Description, decimal Price);

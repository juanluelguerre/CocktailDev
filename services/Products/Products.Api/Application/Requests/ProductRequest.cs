namespace CocktailDev.Products.Api.Application.Requests;

public record ProductRequest(Guid Id, string Name, string Description, decimal Price);

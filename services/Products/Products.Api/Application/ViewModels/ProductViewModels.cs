namespace CocktailDev.Products.Api.Application.ViewModels;

public record struct ProductViewModel(Guid Id, string Name);

public record struct ProductDetailViewModel(
    Guid Id,
    string Name,
    string Description,
    decimal Price);

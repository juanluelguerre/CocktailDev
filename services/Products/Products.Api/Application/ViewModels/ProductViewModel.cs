namespace CocktailDev.Products.Api.Application.ViewModels;

public readonly record struct ProductViewModel(long Id, string Name, string Description,
    string Category);

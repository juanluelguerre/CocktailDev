namespace CocktailDev.Gateway.Application.ViewModels;

[GraphQLName("ProductDetail")]
public record struct ProductDetailViewModel(long Id, string Name, decimal price);

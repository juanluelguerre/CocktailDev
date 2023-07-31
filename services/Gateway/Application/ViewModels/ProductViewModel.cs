namespace CocktailDev.Gateway.Application.ViewModels;

[GraphQLName("Product")]
public record struct ProductViewModel(long Id, string Name);

namespace CocktailDev.Gateway.Application.ViewModels;

[GraphQLName("Order")]
public record struct OrderViewModel(string CustomerName, List<ProductViewModel> Products);

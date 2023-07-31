namespace CocktailDev.Gateway.Application.ViewModels;

[GraphQLName("OrderSummary")]
public record struct OrderDetailViewModel(string CustomerName,
    List<ProductDetailViewModel> Products);

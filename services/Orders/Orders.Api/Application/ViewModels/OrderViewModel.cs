namespace CocktailDev.Orders.Api.Application.ViewModels;

public record struct OrderViewModel(string CustomerName, List<ProductViewModel> Products);

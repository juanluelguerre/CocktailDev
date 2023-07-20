namespace CocktailDev.Gateway.Application.ViewModels;

public record struct OrderViewModel(string CustomerName, List<ProductViewModel> Products);

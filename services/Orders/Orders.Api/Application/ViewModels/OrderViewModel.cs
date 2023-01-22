namespace CocktailDev.Orders.Api.Application.ViewModels;

public readonly record struct OrderViewModel(int OrderNumber, DateTime Date, string Status,
    string Description, string Address, List<OrderItemViewModel> Items, double Total);

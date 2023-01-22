namespace CocktailDev.Orders.Api.Application.ViewModels;

public readonly record struct OrderSummaryViewModel(int OrderNumber, DateTime Date, string Status,
    double Total);

namespace CocktailDev.Orders.Api.Application.ViewModels;

public readonly record struct OrderItemViewModel(string Name, int Units, double UnitPrice,
    string PictureUrl);

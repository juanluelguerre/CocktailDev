namespace CocktailDev.Basket.Api.Application.ViewModels;

public readonly record struct BasketItemViewModel(string Id, int ProductId, string ProductName,
    double UnitPrice, double OldUnitPrice, int Quantity, string PictureUrl);

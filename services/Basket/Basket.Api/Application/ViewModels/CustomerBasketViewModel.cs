namespace CocktailDev.Basket.Api.Application.ViewModels;

public readonly record struct CustomerBasketViewModel(string CustomerId,
    List<BasketItemViewModel> Items);

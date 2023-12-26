using CocktailDev.Orders.Api.Domain.Aggregates.CustomerAggregate;

namespace CocktailDev.Orders.Api.Application.ViewModels;

public record struct OrderViewModel(
    long OrderId,
    DateTime OrderDate,
    Customer Customer,
    List<ProductViewModel> OrderItems,
    decimal TolAmount);

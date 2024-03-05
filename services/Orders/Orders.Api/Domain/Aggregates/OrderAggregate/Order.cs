using CocktailDev.Orders.Api.Domain.Aggregates.CustomerAggregate;

namespace CocktailDev.Orders.Api.Domain.Aggregates.OrderAggregate;

public record Order(
    long Id,
    DateTime OrderDate,
    Customer Customer,
    IReadOnlyCollection<OrderItem> OrderItems,
    decimal TotalAmount);

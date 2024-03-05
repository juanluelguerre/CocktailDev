using CocktailDev.Orders.Api.Domain.Aggregates.OrderAggregate;
using MediatR;

namespace CocktailDev.Orders.Api.Application.Commands;

public record CreateOrderCommand(
    long CustomerId,
    IReadOnlyCollection<long> OrderItemIds)
    : IRequest<Order>;

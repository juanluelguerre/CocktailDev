using CocktailDev.Orders.Api.Application.Requests;
using CocktailDev.Orders.Api.Domain;
using MediatR;

namespace CocktailDev.Orders.Api.Application.Commands;

public record CreateOrderCommand
    (string CustomerName, List<ProductRequest> Products) : IRequest<Order>;

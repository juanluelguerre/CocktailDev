using CocktailDev.Orders.Api.Application.ViewModels;
using MediatR;

namespace CocktailDev.Orders.Api.Application.Queries;

public record GetOrdersQuery : IRequest<List<OrderViewModel>>;

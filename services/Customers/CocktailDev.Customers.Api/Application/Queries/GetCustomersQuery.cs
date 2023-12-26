using CocktailDev.Customers.Api.Application.ViewModels;
using MediatR;

namespace CocktailDev.Customers.Api.Application.Queries;

public record GetCustomersQuery() : IRequest<List<CustomerViewModel>>;

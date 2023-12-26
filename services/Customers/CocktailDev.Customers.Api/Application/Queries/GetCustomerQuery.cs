using CocktailDev.Customers.Api.Application.ViewModels;
using MediatR;

namespace CocktailDev.Customers.Api.Application.Queries;

public record GetCustomerQuery(long Id) : IRequest<CustomerViewModel>;

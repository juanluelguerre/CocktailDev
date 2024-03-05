using MediatR;

namespace CocktailDev.Customers.Api.Application.Commands;

public record UpdateCustomerCommand(long Id, string Name, string Email)
    : IRequest;

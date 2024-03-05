using MediatR;

namespace CocktailDev.Customers.Api.Application.Commands;

public record CreateCustomerCommand(long Id, string Name, string Email)
    : IRequest;

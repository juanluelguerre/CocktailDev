using CocktailDev.Customers.Api.Domain;
using CocktailDev.Customers.Api.Domain.Aggregates.CustomerAggregate;
using MediatR;

namespace CocktailDev.Customers.Api.Application.Commands;

public class CreateCustomerCommandHandler(ICustomerRepository repository)
    : IRequestHandler<CreateCustomerCommand>
{
    public async Task Handle(CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        await repository.AddAsync(new Customer(request.Id, request.Name, request.Email),
            cancellationToken);
    }
}

using CocktailDev.Customers.Api.Domain;
using CocktailDev.Customers.Api.Domain.Aggregates.CustomerAggregate;
using MediatR;

namespace CocktailDev.Customers.Api.Application.Commands;

public class UpdateCustomerCommandHandler(ICustomerRepository repository)
    : IRequestHandler<UpdateCustomerCommand>
{
    public async Task Handle(UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        await repository.UpdateAsync(new Customer(request.Id, request.Name, request.Email),
            cancellationToken);
    }
}

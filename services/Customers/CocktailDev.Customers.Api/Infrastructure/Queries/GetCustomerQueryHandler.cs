using CocktailDev.Customers.Api.Application.Queries;
using CocktailDev.Customers.Api.Application.ViewModels;
using CocktailDev.Customers.Api.Domain.Aggregates;
using MediatR;

namespace CocktailDev.Customers.Api.Infrastructure.Queries;

public class GetCustomerQueryHandler(ICustomerRepository repository)
    : IRequestHandler<GetCustomerQuery, CustomerDetails>
{
    public async Task<CustomerDetails> Handle(GetCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await repository.FindAsync(request.Id, cancellationToken);

        // TODO: Use a custom exception ProductNotFoundException
        if (customer == null)
            throw new KeyNotFoundException(nameof(customer));

        return new CustomerDetails(customer.Id, customer.Name, customer.Email);
    }
}

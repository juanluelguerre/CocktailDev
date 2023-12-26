using CocktailDev.Customers.Api.Application.ViewModels;
using CocktailDev.Customers.Api.Domain;
using MediatR;

namespace CocktailDev.Customers.Api.Application.Queries;

public class GetCustomerQueryHandler(ICustomerRepository repository)
    : IRequestHandler<GetCustomerQuery, CustomerViewModel>
{
    public async Task<CustomerViewModel> Handle(GetCustomerQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await repository.FindAsync(request.Id, cancellationToken);

        // TODO: Use a custom exception ProductNotFoundException
        if (customer == null)
            throw new KeyNotFoundException(nameof(customer));

        return new CustomerViewModel(customer.Id, customer.Name, customer.Email);
    }
}

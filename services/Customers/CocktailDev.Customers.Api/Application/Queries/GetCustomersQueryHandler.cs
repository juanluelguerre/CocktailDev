using CocktailDev.Customers.Api.Application.ViewModels;
using CocktailDev.Customers.Api.Domain;
using MediatR;

namespace CocktailDev.Customers.Api.Application.Queries;

public class GetCustomersQueryHandler(
    ILogger<GetCustomersQueryHandler> logger,
    ICustomerRepository repository)
    : IRequestHandler<GetCustomersQuery, List<CustomerViewModel>>
{
    private readonly ILogger logger = logger;

    public async Task<List<CustomerViewModel>> Handle(GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await repository.GetAllAsync(cancellationToken);
        return customers
            .Select(c => new CustomerViewModel(c.Id, c.Name, c.Email))
            .ToList();
    }
}

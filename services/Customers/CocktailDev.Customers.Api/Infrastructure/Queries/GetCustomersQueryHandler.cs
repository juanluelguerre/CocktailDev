using CocktailDev.Customers.Api.Application.Queries;
using CocktailDev.Customers.Api.Application.ViewModels;
using CocktailDev.Customers.Api.Domain.Aggregates;
using MediatR;

namespace CocktailDev.Customers.Api.Infrastructure.Queries;

public class GetCustomersQueryHandler(
    ILogger<GetCustomersQueryHandler> logger,
    ICustomerRepository repository)
    : IRequestHandler<GetCustomersQuery, List<CustomerDetails>>
{
    private readonly ILogger logger = logger;

    public async Task<List<CustomerDetails>> Handle(GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await repository.GetAllAsync(cancellationToken);
        return customers
            .Select(c => new CustomerDetails(c.Id, c.Name, c.Email))
            .ToList();
    }
}

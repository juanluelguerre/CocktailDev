using CocktailDev.Customers.Api.Domain.Aggregates.CustomerAggregate;

namespace CocktailDev.Customers.Api.Domain;

public interface ICustomerRepository
{
    Task<Customer?> FindAsync(long id, CancellationToken cancellationToken);
    Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Customer customer, CancellationToken cancellationToken);
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken);
}

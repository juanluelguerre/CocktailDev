using CocktailDev.Services.Common.Domain.SeedWork;

namespace CocktailDev.Customers.Api.Domain.Aggregates;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> FindAsync(long id, CancellationToken cancellationToken);
    Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Customer customer, CancellationToken cancellationToken);
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken);
}

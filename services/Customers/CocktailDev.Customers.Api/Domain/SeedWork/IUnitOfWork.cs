namespace CocktailDev.Customers.Api.Domain.SeedWork;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

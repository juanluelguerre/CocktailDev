namespace CocktailDev.Orders.Api.Domain.Aggregates.CustomerAggregate;

public interface ICustomerRepository
{
    Task<Customer?> FindByIdAsync(long id);
}

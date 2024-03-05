using CocktailDev.Orders.Api.Domain.Aggregates.OrderAggregate;

namespace CocktailDev.Orders.Api.Domain.Aggregates.ProductAggregate;

public interface IProductRepository
{
    Task<List<OrderItem>> FindByIdsAsync(long[] itemsId);
}

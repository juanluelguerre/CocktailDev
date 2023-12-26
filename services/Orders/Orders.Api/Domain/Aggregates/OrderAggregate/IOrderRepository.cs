namespace CocktailDev.Orders.Api.Domain.Aggregates.OrderAggregate;

public interface IOrderRepository
{
    Task<List<Order>> GetOrdersAsync();
    Task<Order> AddAsync(Order order);
}

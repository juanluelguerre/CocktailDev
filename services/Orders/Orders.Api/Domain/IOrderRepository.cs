namespace CocktailDev.Orders.Api.Domain;

public interface IOrderRepository
{
    Task<List<Order>> GetOrdersAsync();
    Task<Order> CreateOrderAsync(Order order);
}

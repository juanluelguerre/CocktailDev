namespace CocktailDev.Gateway.Domain;

public interface IOrderRepository
{
    Task<List<Order>> GetOrdersAsync();
}

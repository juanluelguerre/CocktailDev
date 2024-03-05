using CocktailDev.Gateway.Domain;

namespace CocktailDev.Gateway.Infrastructure.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> orders;

    public InMemoryOrderRepository()
    {
        this.orders = GenerateSampleOrders();
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        return this.orders;
    }

    private List<Order> GenerateSampleOrders()
    {
        var orders = new List<Order>();

        var order1 = new Order("John Doe", new List<Product>
            {
                new(1, "Product 1"),
                new(2, "Product 2")
            }
        );
        orders.Add(order1);

        var order2 = new Order("", new List<Product>
            {
                new(1, "Product 1"),
                new(2, "Product 2"),
                new(3, "Product 3")
            }
        );
        orders.Add(order2);

        return orders;
    }
}

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

        var order1 = new Order
        {
            OrderId = 1,
            CustomerName = "John Doe",
            Products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product 1", Price = 10m },
                new Product { ProductId = 2, Name = "Product 2", Price = 20m }
            }
        };
        orders.Add(order1);

        var order2 = new Order
        {
            OrderId = 2,
            CustomerName = "Jane Smith",
            Products = new List<Product>
            {
                new Product { ProductId = 3, Name = "Product 3", Price = 30m },
                new Product { ProductId = 4, Name = "Product 4", Price = 40m },
                new Product { ProductId = 5, Name = "Product 5", Price = 50m }
            }
        };
        orders.Add(order2);

        return orders;
    }
}

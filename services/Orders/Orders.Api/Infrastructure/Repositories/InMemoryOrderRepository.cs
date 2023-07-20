using CocktailDev.Orders.Api.Domain;

namespace CocktailDev.Orders.Api.Infrastructure.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> orders;

    public InMemoryOrderRepository()
    {
        this.orders = new List<Order>();
        this.InitializeData();
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        return this.orders;
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        this.orders.Add(order);
        return order;
    }

    private void InitializeData()
    {
        var products = new List<Product>
        {
            new(1, "Laptop"),
            new(2, "Mouse"),
            new(3, "Camera"),
            new(4, "Keyboard"),
            new(5, "Microphone")
        };

        this.orders.Add(new Order("ElGuerre.com", products));
    }
}

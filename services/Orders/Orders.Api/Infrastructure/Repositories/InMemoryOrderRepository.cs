using CocktailDev.Orders.Api.Domain.Aggregates.CustomerAggregate;
using CocktailDev.Orders.Api.Domain.Aggregates.OrderAggregate;

namespace CocktailDev.Orders.Api.Infrastructure.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> orders;

    public InMemoryOrderRepository()
    {
        this.orders = [];
        this.InitializeData();
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        return await Task.FromResult(this.orders);
    }

    public async Task<Order> AddAsync(Order order)
    {
        this.orders.Add(order);
        return await Task.FromResult(order);
    }

    private void InitializeData()
    {
        var products = new List<OrderItem>
        {
            new(1, "Laptop", 1200),
            new(2, "Mouse", 89),
            new(3, "Camera", 750),
            new(4, "Keyboard", 115),
            new(5, "Microphone", 58)
        };

        this.orders.Add(new Order(1, DateTime.UtcNow,
            new Customer(1, "JuanLuElGuerre", "juanlu@elguerre.com"), products,
            products.Sum(i => i.Price)));
    }
}

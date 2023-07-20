using CocktailDev.Gateway.Application.ViewModels;
using CocktailDev.Gateway.Domain;
using HotChocolate.Caching;

namespace CocktailDev.Gateway.Application.Queries;

[ExtendObjectType("Query")]
public class OrdersQuery
{
    private readonly IOrderRepository orderRepository;

    public OrdersQuery(IOrderRepository productRepository)
    {
        this.orderRepository = productRepository;
    }

    [CacheControl(maxAge: 10_000)]
    public async Task<List<OrderViewModel>> GetOrders()
    {
        var orders = await this.orderRepository.GetOrdersAsync();
        return orders.Select(o => new OrderViewModel(o.CustomerName,
            o.Products.Select(p => new ProductViewModel(p.Id, p.Name)).ToList())).ToList();
    }
}

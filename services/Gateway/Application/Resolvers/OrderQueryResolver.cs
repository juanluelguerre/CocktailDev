using CocktailDev.Gateway.Domain;

namespace CocktailDev.Gateway.Application.Resolvers;

public class OrderQueryResolver : IOrderQueryResolver
{
    private readonly IOrderRepository orderRepository;

    public OrderQueryResolver(IOrderRepository productRepository)
    {
        this.orderRepository = productRepository;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        return await this.orderRepository.GetOrdersAsync();
    }
}

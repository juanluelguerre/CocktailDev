using CocktailDev.Gateway.Application.Builders;
using CocktailDev.Gateway.Application.ViewModels;
using CocktailDev.Gateway.Domain;
using HotChocolate.Caching;

namespace CocktailDev.Gateway.Application.Queries;

[ExtendObjectType("Query")]
public class OrderSummaryQuery
{
    private readonly IProductRepository productRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IOrderDetailsBuilder orderDetailsBuilder;

    public OrderSummaryQuery(IOrderRepository orderRepository, IProductRepository productRepository,
        IOrderDetailsBuilder orderDetailsBuilder)
    {
        this.orderRepository = orderRepository;
        this.productRepository = productRepository;
        this.orderDetailsBuilder = orderDetailsBuilder;
    }

    [CacheControl(maxAge: 10_000)]
    public async Task<List<OrderDetailViewModel>> GetSummary()
    {
        var orders = await this.orderRepository.GetOrdersAsync();
        return await this.orderDetailsBuilder.BuildOrderDetails(orders);
    }

    [CacheControl(maxAge: 10_000)]
    public async Task<List<OrderDetailViewModel>> GetSummaryByCustomerName(string customerName)
    {
        var orders = (await this.orderRepository.GetOrdersAsync())
            .Where(o =>
                o.CustomerName.Equals(customerName, StringComparison.InvariantCultureIgnoreCase));

        return await this.orderDetailsBuilder.BuildOrderDetails(orders);
    }
}

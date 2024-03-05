using CocktailDev.Gateway.Domain;

namespace CocktailDev.Gateway.Application.Resolvers;

[ExtendObjectType("Query")]
public class OrderSummaryResolver : ObjectType<OrderSummary>
{
    private readonly IProductRepository productRepository;
    private readonly IOrderRepository orderRepository;

    public OrderSummaryResolver(IProductRepository productRepository,
        IOrderRepository orderRepository)
    {
        this.productRepository = productRepository;
        this.orderRepository = orderRepository;
    }

    protected override void Configure(IObjectTypeDescriptor<OrderSummary> descriptor)
    {
        descriptor.Field(f => f.CustomerName)
            .Resolve((context, ct) => context.Parent<Order>().CustomerName);

        descriptor.Field(f => f.Products)
            .Resolve(async (context, ct) =>
            {
                var prodId = context.Parent<ProductDetail>().Id;
                var productsDetail = await this.productRepository.FindProductAsync(prodId);


                // return new OrderSummary(context.Parent<Order>().CustomerName, productsDetail) ;
                return productsDetail;
            });
    }
}

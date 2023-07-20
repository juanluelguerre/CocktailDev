using CocktailDev.Gateway.Domain;

namespace CocktailDev.Gateway.Application.Resolvers;

// [ExtendObjectType("Query")]
public class OrderSummaryResolver : ObjectType<OrderSummary>
{
    private readonly IProductRepository productRepository;

    public OrderSummaryResolver(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    protected override void Configure(IObjectTypeDescriptor<OrderSummary> descriptor)
    {
        descriptor.Field(f => f.Products)
            .Resolve(async (context, ct) =>
            {
                var prodId = context.Parent<ProductDetail>().Id;
                var productsDetail = await this.productRepository.FindProductAsync(prodId);


                return productsDetail;
            });
    }
}

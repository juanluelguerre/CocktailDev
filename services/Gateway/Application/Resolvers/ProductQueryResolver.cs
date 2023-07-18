using CocktailDev.Gateway.Domain;

namespace CocktailDev.Gateway.Application.Resolvers;

public class ProductQueryResolver : IProductQueryResolver
{
    private readonly IProductRepository productRepository;

    public ProductQueryResolver(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await this.productRepository.GetProductsAsync();
    }
}

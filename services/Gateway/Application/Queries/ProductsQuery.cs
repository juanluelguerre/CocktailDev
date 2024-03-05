using CocktailDev.Gateway.Application.ViewModels;
using CocktailDev.Gateway.Domain;
using HotChocolate.Caching;

namespace CocktailDev.Gateway.Application.Queries;

[ExtendObjectType("Query")]
public class ProductsQuery
{
    private readonly IProductRepository productRepository;

    public ProductsQuery(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    [CacheControl(maxAge: 10_000)]
    public async Task<List<ProductViewModel>> GetProducts()
    {
        var products = await this.productRepository.GetProductsAsync();
        return products.Select(p => new ProductViewModel(p.Id, p.Name)).ToList();
    }
}

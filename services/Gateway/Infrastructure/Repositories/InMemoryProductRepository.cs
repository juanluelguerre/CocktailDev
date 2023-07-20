using CocktailDev.Gateway.Domain;

namespace CocktailDev.Gateway.Infrastructure.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<ProductDetail> products;

    public InMemoryProductRepository()
    {
        this.products = GenerateSampleProducts();
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await Task.FromResult(this.products.Select(p => new Product(p.Id, p.Name)).ToList());
    }

    public async Task<ProductDetail?> FindProductAsync(long id)
    {
        return await Task.FromResult(this.products.FirstOrDefault(p => p.Id == id));
    }

    private static List<ProductDetail> GenerateSampleProducts()
    {
        var products = new List<ProductDetail>();
        for (var i = 1; i <= 50; i++)
        {
            products.Add(new(i, $"Product {i}", i * 10m));
        }

        return products;
    }
}

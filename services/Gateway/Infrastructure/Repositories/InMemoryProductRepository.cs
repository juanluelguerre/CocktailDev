using CocktailDev.Gateway.Domain;

namespace CocktailDev.Gateway.Infrastructure.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> products;

    public InMemoryProductRepository()
    {
        this.products = GenerateSampleProducts();
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return this.products;
    }

    private static List<Product> GenerateSampleProducts()
    {
        var products = new List<Product>();
        for (var i = 1; i <= 50; i++)
        {
            products.Add(new Product
            {
                ProductId = i,
                Name = $"Product {i}",
                Price = i * 10m
            });
        }

        return products;
    }
}

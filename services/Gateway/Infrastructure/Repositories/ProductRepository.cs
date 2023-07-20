using System.Text.Json;
using CocktailDev.Gateway.Domain;

namespace CocktailDev.Gateway.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly HttpClient httpClient;

    public ProductRepository(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<ProductDetail?> FindProductAsync(long id)
    {
        var response = await this.httpClient.GetAsync($"/api/product/{id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var product = JsonSerializer.Deserialize<ProductDetail>(content);

        return product;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        var response = await this.httpClient.GetAsync("/api/products");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<Product>>(content);

        return products ?? new List<Product>();
    }
}

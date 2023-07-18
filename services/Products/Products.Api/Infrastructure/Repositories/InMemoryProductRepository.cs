using CocktailDev.Products.Api.Domain;

namespace CocktailDev.Products.Api.Infrastructure.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> products;

    public InMemoryProductRepository()
    {
        this.products = new List<Product>();
        this.InitializeData();
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return this.products;
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        this.products.Add(new Product(this.products.Count + 1, product.Name, product.Price));
        return product;
    }

    private void InitializeData()
    {
        this.products.Add(new Product(1, "Laptop", 1100));
        this.products.Add(new Product(2, "Mouse", 50));
        this.products.Add(new Product(3, "Camera", 45));
        this.products.Add(new Product(4, "Keyboard", 99.5m));
        this.products.Add(new Product(5, "Microphone", 67.99m));
    }
}

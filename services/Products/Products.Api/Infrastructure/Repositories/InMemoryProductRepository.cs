using CocktailDev.Products.Api.Domain;

namespace CocktailDev.Products.Api.Infrastructure.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<ProductDetail> products;

    public InMemoryProductRepository()
    {
        this.products = new List<ProductDetail>();
        this.InitializeData();
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return this.products.Select(p => new Product(p.Id, p.Name)).ToList();
    }

    public async Task<ProductDetail?> FindProductAsync(long id)
    {
        return await Task.FromResult(this.products.FirstOrDefault(p => p.Id == id));
    }

    public async Task<ProductDetail> CreateProductAsync(ProductDetail product)
    {
        this.products.Add(new ProductDetail(this.products.Count + 1, product.Name, product.Price));
        return product;
    }

    private void InitializeData()
    {
        this.products.Add(new ProductDetail(1, "Laptop", 1100));
        this.products.Add(new ProductDetail(2, "Mouse", 50));
        this.products.Add(new ProductDetail(3, "Camera", 45));
        this.products.Add(new ProductDetail(4, "Keyboard", 99.5m));
        this.products.Add(new ProductDetail(5, "Microphone", 67.99m));
    }
}

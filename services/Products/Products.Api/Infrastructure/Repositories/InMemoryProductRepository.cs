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
        this.products.Add(new ProductDetail(this.products.Count + 1, product.Name,
            product.Description, product.Price));
        return await Task.FromResult(product);
    }

    private void InitializeData()
    {
        this.products.Add(new ProductDetail(1, "Laptop", "Really nice portable computer", 1100));
        this.products.Add(new ProductDetail(2, "Smartphone",
            "High-end mobile device with advanced features", 800));
        this.products.Add(new ProductDetail(3, "Headphones",
            "Premium noise-canceling headphones for immersive audio", 200));
        this.products.Add(new ProductDetail(4, "Digital Camera",
            "Professional-grade camera for stunning photography", 1200));
        this.products.Add(new ProductDetail(5, "Fitness Tracker",
            "Track your health and fitness activities with precision", 100));
        this.products.Add(new ProductDetail(6, "Smart TV",
            "Ultra HD smart television with a sleek design", 1500));
        this.products.Add(new ProductDetail(7, "Gaming Console",
            "Powerful gaming console for an immersive gaming experience", 500));
        this.products.Add(new ProductDetail(8, "Wireless Speaker",
            "High-quality wireless speaker for crystal-clear audio", 150));
        this.products.Add(new ProductDetail(9, "Tablet",
            "Versatile tablet for work and entertainment on the go", 600));
        this.products.Add(new ProductDetail(10, "Coffee Maker",
            "State-of-the-art coffee maker for the perfect brew", 80));
    }
}

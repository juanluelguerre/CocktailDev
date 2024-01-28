using CocktailDev.Products.Api.Domain.Aggregates;
using CocktailDev.Products.Api.Infrastructure.EntityFramework;
using CocktailDev.Services.Common.Infrastructure.Repositories;

namespace CocktailDev.Products.Api.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<ProductContext, Product>, IProductRepository
{
    private readonly List<Product> products = [];

    public ProductRepository(ProductContext context) : base(context)
    {
        this.InitializeData();

        context.Set<Product>().AddRange(this.products);
        context.SaveChanges();
    }

    private void InitializeData()
    {
        this.products.Add(Product.Create(Guid.NewGuid(), "Laptop", "Really nice portable computer",
            1100));
        this.products.Add(Product.Create(Guid.NewGuid(), "Smartphone",
            "High-end mobile device with advanced features", 800));
        this.products.Add(Product.Create(Guid.NewGuid(), "Headphones",
            "Premium noise-canceling headphones for immersive audio", 200));
        this.products.Add(Product.Create(Guid.NewGuid(), "Digital Camera",
            "Professional-grade camera for stunning photography", 1200));
        this.products.Add(Product.Create(Guid.NewGuid(), "Fitness Tracker",
            "Track your health and fitness activities with precision", 100));
        this.products.Add(Product.Create(Guid.NewGuid(), "Smart TV",
            "Ultra HD smart television with a sleek design", 1500));
        this.products.Add(Product.Create(Guid.NewGuid(), "Gaming Console",
            "Powerful gaming console for an immersive gaming experience", 500));
        this.products.Add(Product.Create(Guid.NewGuid(), "Wireless Speaker",
            "High-quality wireless speaker for crystal-clear audio", 150));
        this.products.Add(Product.Create(Guid.NewGuid(), "Tablet",
            "Versatile tablet for work and entertainment on the go", 600));
        this.products.Add(Product.Create(Guid.NewGuid(), "Coffee Maker",
            "State-of-the-art coffee maker for the perfect brew", 80));
    }
}

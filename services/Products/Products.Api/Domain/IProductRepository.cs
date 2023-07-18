namespace CocktailDev.Products.Api.Domain;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
    Task<Product> CreateProductAsync(Product product);
}

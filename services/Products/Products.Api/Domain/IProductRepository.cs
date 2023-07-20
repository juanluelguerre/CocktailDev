namespace CocktailDev.Products.Api.Domain;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
    Task<ProductDetail?> FindProductAsync(long id);
    Task<ProductDetail> CreateProductAsync(ProductDetail product);
}

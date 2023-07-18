namespace CocktailDev.Gateway.Domain;

public interface IProductRepository
{
    Task<List<Product>> GetProductsAsync();
}

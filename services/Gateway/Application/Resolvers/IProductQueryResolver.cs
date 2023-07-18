using CocktailDev.Gateway.Domain;

namespace CocktailDev.Gateway.Application.Resolvers;

public interface IProductQueryResolver
{
    Task<List<Product>> GetProductsAsync();
}

using CocktailDev.Gateway.Domain;

namespace CocktailDev.Gateway.Application.Resolvers;

public interface IOrderQueryResolver
{
    Task<List<Order>> GetOrdersAsync();
}

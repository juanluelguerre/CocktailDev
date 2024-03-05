using CocktailDev.Gateway.Application.ViewModels;
using CocktailDev.Gateway.Domain;

namespace CocktailDev.Gateway.Application.Builders;

public interface IOrderDetailsBuilder
{
    Task<List<OrderDetailViewModel>> BuildOrderDetails(IEnumerable<Order> orders);
}

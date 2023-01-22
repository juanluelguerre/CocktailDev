using CocktailDev.Basket.Api.Application.ViewModels;
using CocktailDev.Basket.Api.Domain;

namespace CocktailDev.Basket.Api.Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    public Task<CustomerBasketViewModel> GetAsync(string customerId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}

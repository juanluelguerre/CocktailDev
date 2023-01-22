using CocktailDev.Basket.Api.Application.ViewModels;

namespace CocktailDev.Basket.Api.Domain
{
    public interface IBasketRepository
    {
        Task<CustomerBasketViewModel> GetAsync(string customerId);
        Task<bool> DeleteAsync(string id);
    }
}

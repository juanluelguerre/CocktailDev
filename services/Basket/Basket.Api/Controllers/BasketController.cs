using CocktailDev.Basket.Api.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CocktailDev.Basket.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BasketController : ControllerBase
{
    private readonly ILogger<BasketController> logger;

    public BasketController(ILogger<BasketController> logger)
    {
        this.logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<CustomerBasketViewModel> Get()
    {
        this.logger.LogTrace("Getting Basket");

        var items = new List<BasketItemViewModel>()
        {
            new(Guid.NewGuid().ToString("N"), 1, "Laptop", 1100, 1250, 1, ""),
            new(Guid.NewGuid().ToString("N"), 2, "Mouse", 50, 48.50, 1, "")
        };

        return Enumerable.Range(1, 1)
            .Select(index => new CustomerBasketViewModel("Customer1", items))
            .ToArray();
    }
}

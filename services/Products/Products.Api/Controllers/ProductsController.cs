using CocktailDev.Products.Api.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CocktailDev.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> logger;

    public ProductsController(ILogger<ProductsController> logger)
    {
        this.logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<ProductViewModel> Get()
    {
        this.logger.LogTrace("Getting Products");

        return Enumerable.Range(1, 1)
            .Select(index => new ProductViewModel(1, "Laptop", "Dell XPS 15", "Computing"))
            .ToArray();
    }
}

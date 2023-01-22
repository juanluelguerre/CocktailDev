using CocktailDev.Orders.Api.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CocktailDev.Orders.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> logger;

    public OrdersController(ILogger<OrdersController> logger)
    {
        this.logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<OrderViewModel> Get()
    {
        this.logger.LogTrace("Getting Orders");

        var orderItems = new List<OrderItemViewModel>()
        {
            new("Laptop", 1, 1100, ""),
            new("Mouse", 1, 50, ""),
            new("Keyboard", 1, 99.5, ""),
            new("Camera", 1, 45, ""),
            new("Microphone", 1, 67.99, ""),
        };

        return Enumerable.Range(1, 1).Select(i => new OrderViewModel(Random.Shared.Next(1, 99),
            DateTime.Now, "Initiated", "My first Setup", "Washington Square, 125, 1B. Madrid",
            orderItems, 100)).ToArray();
    }
}

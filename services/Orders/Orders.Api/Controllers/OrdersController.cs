using CocktailDev.Orders.Api.Application.Commands;
using CocktailDev.Orders.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CocktailDev.Orders.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> logger;
    private readonly IMediator mediator;

    public OrdersController(ILogger<OrdersController> logger, IMediator mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        this.logger.LogTrace("Getting Orders");

        // TODO: We can use here a factory repository to delegate query creation
        var query = new GetOrdersQuery();
        var orders = await this.mediator.Send(query);
        return this.Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        // TODO: We can use here a factory repository to delegate command creation
        var result = await this.mediator.Send(command);
        // Additional logic if needed
        return this.Ok(result);
    }
}

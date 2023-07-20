using CocktailDev.Products.Api.Application.Commands;
using CocktailDev.Products.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CocktailDev.Products.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IMediator mediator;

    public ProductsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        // TODO: We can use here a factory repository to delegate query creation
        var query = new GetProductsQuery();
        var products = await this.mediator.Send(query);
        return this.Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(long id)
    {
        // TODO: We can use here a factory repository to delegate query creation
        var query = new GetProductQuery(id);
        var products = await this.mediator.Send(query);
        return this.Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        // TODO: We can use here a factory repository to delegate command creation
        var result = await this.mediator.Send(command);
        return this.Ok(result);
    }
}

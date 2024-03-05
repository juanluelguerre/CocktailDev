using CocktailDev.Products.Api.Application.Commands;
using CocktailDev.Products.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CocktailDev.Products.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        // TODO: We can use here a factory repository to delegate query creation
        var query = new GetProductsQuery();
        var products = await mediator.Send(query);
        return this.Ok(products);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        // TODO: We can use here a factory repository to delegate query creation
        var query = new GetProductQuery(id);
        var product = await mediator.Send(query);
        return this.Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        // TODO: We can use here a factory repository to delegate command creation
        var result = await mediator.Send(command);
        return this.Ok(result);
    }
}

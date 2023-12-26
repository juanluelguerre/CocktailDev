using CocktailDev.Customers.Api.Application.Commands;
using CocktailDev.Customers.Api.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CocktailDev.Customers.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var query = new GetCustomersQuery();
        var customers = await mediator.Send(query);
        return this.Ok(customers);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetCustomer(long id)
    {
        var query = new GetCustomerQuery(id);
        var customer = await mediator.Send(query);
        return this.Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
    {
        await mediator.Send(command);
        return this.NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCustomer(UpdateCustomerCommand command)
    {
        await mediator.Send(command);
        return this.NoContent();
    }
}

using CocktailDev.Orders.Api.Application.Queries;
using CocktailDev.Orders.Api.Application.ViewModels;
using CocktailDev.Orders.Api.Domain.Aggregates.OrderAggregate;
using MediatR;

namespace CocktailDev.Orders.Api.Infrastructure.Queries;

public class GetProductsQueryHandler(IOrderRepository repository)
    : IRequestHandler<GetOrdersQuery, List<OrderViewModel>>
{
    public async Task<List<OrderViewModel>> Handle(GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await repository.GetOrdersAsync();
        return orders.Select(o => new OrderViewModel(o.Id, o.OrderDate, o.Customer,
                o.OrderItems.Select(p => new ProductDetails(p.Id, p.Name)).ToList(),
                o.TotalAmount))
            .ToList();
    }
}

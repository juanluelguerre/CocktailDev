using CocktailDev.Orders.Api.Application.ViewModels;
using CocktailDev.Orders.Api.Domain;
using MediatR;

namespace CocktailDev.Orders.Api.Application.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderViewModel>>
{
    private readonly IOrderRepository orderRepository;

    public GetProductsQueryHandler(IOrderRepository productRepository)
    {
        this.orderRepository = productRepository;
    }

    public async Task<List<OrderViewModel>> Handle(GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await this.orderRepository.GetOrdersAsync();
        return orders.Select(o => new OrderViewModel(o.CustomerName,
            o.Products.Select(p => new ProductViewModel(p.Id, p.Name)).ToList())).ToList();
    }
}

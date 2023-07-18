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
        var products = await this.orderRepository.GetOrdersAsync();
        return products.Select(o => new OrderViewModel(o.CustomerName,
            o.Products.Select(p => new ProductViewModel(p.Id, p.Name, p.Price)).ToList())).ToList();
    }
}

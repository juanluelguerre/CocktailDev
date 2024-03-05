using CocktailDev.Orders.Api.Domain.Aggregates.CustomerAggregate;
using CocktailDev.Orders.Api.Domain.Aggregates.OrderAggregate;
using CocktailDev.Orders.Api.Domain.Aggregates.ProductAggregate;
using MediatR;

namespace CocktailDev.Orders.Api.Application.Commands;

public class CreateOrderCommandHandler(
    IOrderRepository repository,
    ICustomerRepository customerRepository,
    IProductRepository productRepository)
    : IRequestHandler<CreateOrderCommand, Order>
{
    public async Task<Order> Handle(CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await customerRepository.FindByIdAsync(request.CustomerId) ??
                       throw new KeyNotFoundException(
                           $"Customer with id {request.CustomerId} not found");

        var products = await productRepository.FindByIdsAsync([.. request.OrderItemIds]);
        if (products.Count != request.OrderItemIds.Count)
            throw new KeyNotFoundException("One or more products not found");

        var orderItems = products.Select(p => new OrderItem(p.Id, p.Name, p.Price)).ToList();
        var order = new Order(1, DateTime.UtcNow, customer, orderItems,
            orderItems.Sum(i => i.Price));

        var createdOrder = await repository.AddAsync(order);
        return createdOrder;
    }
}

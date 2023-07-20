using CocktailDev.Orders.Api.Domain;
using MediatR;

namespace CocktailDev.Orders.Api.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepository orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<Order> Handle(CreateOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = new Order(request.CustomerName,
                request.Products.Select(p => new Product(p.Id, p.Name)).ToList());

            var createdOrder = await this.orderRepository.CreateOrderAsync(order);
            return createdOrder;
        }
    }
}

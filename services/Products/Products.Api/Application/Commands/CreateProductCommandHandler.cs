using CocktailDev.Products.Api.Domain.Aggregates;
using MediatR;

namespace CocktailDev.Products.Api.Application.Commands;

public class CreateProductCommandHandler(IProductRepository repository)
    : IRequestHandler<CreateProductCommand, Product>
{
    public async Task<Product> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = Product.Create(request.Id, request.Name, request.Description, request.Price);

        return await repository.AddAsync(product, cancellationToken);
    }
}

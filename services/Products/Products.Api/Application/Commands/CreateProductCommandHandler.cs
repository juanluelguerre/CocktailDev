using CocktailDev.Products.Api.Application.Requests;
using CocktailDev.Products.Api.Domain;
using MediatR;

namespace CocktailDev.Products.Api.Application.Commands;

public class CreateProductCommandHandler(IProductRepository repository)
    : IRequestHandler<CreateProductCommand, ProductDetail>
{
    public async Task<ProductDetail> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product =
            new ProductRequest(request.Id, request.Name, request.Description, request.Price);
        var createdProduct =
            await repository.CreateProductAsync(
                new ProductDetail(product.Id, product.Name, product.Description, product.Price));
        return createdProduct;
    }
}

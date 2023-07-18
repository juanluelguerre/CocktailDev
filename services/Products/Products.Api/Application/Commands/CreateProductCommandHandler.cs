using CocktailDev.Products.Api.Application.Requests;
using CocktailDev.Products.Api.Domain;
using MediatR;

namespace CocktailDev.Products.Api.Application.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Product> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = new ProductRequest(request.Id, request.Name, request.Price);
        var createdProduct =
            await this.productRepository.CreateProductAsync(
                new Product(product.Id, product.Name, product.Price));
        return createdProduct;
    }
}

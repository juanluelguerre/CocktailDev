using CocktailDev.Products.Api.Application.ViewModels;
using CocktailDev.Products.Api.Domain;
using MediatR;

namespace CocktailDev.Products.Api.Application.Queries;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDetailViewModel>
{
    private readonly IProductRepository productRepository;

    public GetProductQueryHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<ProductDetailViewModel> Handle(GetProductQuery request,
        CancellationToken cancellationToken)
    {
        var product = await this.productRepository.FindProductAsync(request.Id);

        // TODO: Use a custom exception ProductNotFoundException
        if (product == null)
            throw new KeyNotFoundException(nameof(product));

        return new ProductDetailViewModel(product.Value.Id, product.Value.Name,
            product.Value.Price);
    }
}

using CocktailDev.Products.Api.Application.Queries;
using CocktailDev.Products.Api.Application.ViewModels;
using CocktailDev.Products.Api.Domain.Aggregates;
using MediatR;

namespace CocktailDev.Products.Api.Infrastructure.Queries;

public class GetProductQueryHandler(IProductRepository repository)
    : IRequestHandler<GetProductQuery, ProductDetailViewModel>
{
    public async Task<ProductDetailViewModel> Handle(GetProductQuery request,
        CancellationToken cancellationToken)
    {
        var product = await repository.FindAsync(request.Id, cancellationToken);

        // TODO: Use a custom exception ProductNotFoundException
        if (product == null)
            throw new KeyNotFoundException(nameof(product));

        return new ProductDetailViewModel(product.Id, product.Name, product.Description,
            product.Price);
    }
}

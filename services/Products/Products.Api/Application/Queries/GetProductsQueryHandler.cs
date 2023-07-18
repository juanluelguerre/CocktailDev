using CocktailDev.Products.Api.Application.ViewModels;
using CocktailDev.Products.Api.Domain;
using MediatR;

namespace CocktailDev.Products.Api.Application.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductViewModel>>
{
    private readonly IProductRepository productRepository;

    public GetProductsQueryHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<List<ProductViewModel>> Handle(GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await this.productRepository.GetProductsAsync();
        return products.Select(p => new ProductViewModel(p.Id, p.Name, p.Price)).ToList();
    }
}

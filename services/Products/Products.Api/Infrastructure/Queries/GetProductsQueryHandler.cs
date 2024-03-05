using CocktailDev.Products.Api.Application.ViewModels;
using CocktailDev.Products.Api.Domain;
using CocktailDev.Products.Api.Domain.Aggregates;
using CocktailDev.Products.Api.Infrastructure.EntityFramework;
using MediatR;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace CocktailDev.Products.Api.Application.Queries;

public class GetProductsQueryHandler(
    ILogger<GetProductsQueryHandler> logger,
    ProductContext context)
    : IRequestHandler<GetProductsQuery, List<ProductViewModel>>
{
    private readonly ILogger logger = logger;

    public async Task<List<ProductViewModel>> Handle(GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Querying Products");

        try
        {
            var random = new Random();
            if (random.Next(0, 5) < 2)
                throw new Exception("Some random error happened !!!");

            var products = context.Set<Product>().ToList();
            return products.Select(p => new ProductViewModel(p.Id, p.Name)).ToList();
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Something was really wrong !");
            throw;
        }
    }
}

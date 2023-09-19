using System.Diagnostics;
using CocktailDev.Products.Api.Application.ViewModels;
using CocktailDev.Products.Api.Domain;
using MediatR;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace CocktailDev.Products.Api.Application.Queries;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductViewModel>>
{
    private readonly ILogger logger;
    private readonly IProductRepository productRepository;

    public GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger,
        IProductRepository productRepository)
    {
        this.logger = logger;
        this.productRepository = productRepository;
    }

    public async Task<List<ProductViewModel>> Handle(GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Querying Products");

        try
        {
            var random = new Random();
            if (random.Next(0, 5) < 2)
                throw new Exception("Some random error happened !!!");

            var products = await this.productRepository.GetProductsAsync();
            return products.Select(p => new ProductViewModel(p.Id, p.Name)).ToList();
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Something was really wrong !");
            throw;
        }
    }
}

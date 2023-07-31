using CocktailDev.Gateway.Application.ViewModels;
using CocktailDev.Gateway.Domain;

namespace CocktailDev.Gateway.Application.Builders;

public class OrderDetailsBuilder : IOrderDetailsBuilder
{
    private readonly IProductRepository productRepository;

    public OrderDetailsBuilder(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<List<OrderDetailViewModel>> BuildOrderDetails(IEnumerable<Order> orders)
    {
        var ordersDetail = new List<OrderDetailViewModel>();

        foreach (var order in orders)
        {
            var products = new List<ProductDetailViewModel>();
            foreach (var product in order.Products)
            {
                var productDetail = await this.productRepository.FindProductAsync(product.Id);
                if (productDetail is not null)
                {
                    products.Add(new ProductDetailViewModel(productDetail.Id, productDetail.Name,
                        productDetail.Price));
                }
            }

            ordersDetail.Add(new OrderDetailViewModel(order.CustomerName, products));
        }

        return ordersDetail;
    }
}

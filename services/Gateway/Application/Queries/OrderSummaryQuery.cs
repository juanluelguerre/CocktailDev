using CocktailDev.Gateway.Application.Models;
using CocktailDev.Gateway.Application.Resolvers;
using GraphQL.Types;

namespace CocktailDev.Gateway.Application.Queries
{
    public class OrderSummaryQuery : ObjectGraphType
    {
        public OrderSummaryQuery(IProductQueryResolver productQueryResolver,
            IOrderQueryResolver orderQueryResolver)
        {
            this.Name = "Query";

            this.Field<ListGraphType<ProductType>>(
                name: "products",
                resolve: context => productQueryResolver.GetProductsAsync()
            );

            this.Field<ListGraphType<OrderType>>(
                name: "orders",
                resolve: context => orderQueryResolver.GetOrdersAsync()
            );
        }
    }
}

using CocktailDev.Gateway.Domain;
using GraphQL.Types;

namespace CocktailDev.Gateway.Application.Models;

public sealed class OrderType : ObjectGraphType<Order>
{
    public OrderType()
    {
        this.Field(x => x.OrderId);
        this.Field(x => x.CustomerName);
        this.Field<ListGraphType<ProductType>>(
            name: "products",
            resolve: context => context.Source.Products
        );
    }
}

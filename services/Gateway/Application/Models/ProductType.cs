using CocktailDev.Gateway.Domain;
using GraphQL.Types;

namespace CocktailDev.Gateway.Application.Models;

public sealed class ProductType : ObjectGraphType<Product>
{
    public ProductType()
    {
        this.Field(x => x.ProductId);
        this.Field(x => x.Name);
        this.Field(x => x.Price);
    }
}

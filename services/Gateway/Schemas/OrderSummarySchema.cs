using CocktailDev.Gateway.Application.Queries;
using GraphQL.Instrumentation;
using GraphQL.Types;

namespace CocktailDev.Gateway.Schemas;

public class OrderSummarySchema : Schema
{
    public OrderSummarySchema(IServiceProvider provider) : base(provider)
    {
        Query = (OrderSummaryQuery)provider.GetService(typeof(OrderSummaryQuery)) ??
                throw new InvalidOperationException();
        //Mutation = (OrderSummaryMutation)provider.GetService(typeof(OrderSummaryMutation)) ?? throw new InvalidOperationException();
        FieldMiddleware.Use(new InstrumentFieldsMiddleware());
    }
}

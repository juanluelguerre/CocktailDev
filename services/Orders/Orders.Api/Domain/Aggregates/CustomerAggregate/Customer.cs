namespace CocktailDev.Orders.Api.Domain.Aggregates.CustomerAggregate;

public record struct Customer(long Id, string Name, string Email);

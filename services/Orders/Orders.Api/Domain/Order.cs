namespace CocktailDev.Orders.Api.Domain;

public record Order(string CustomerName, List<Product> Products);

namespace CocktailDev.Orders.Api.Application.Requests;

public record OrderRequest(string CustomerName, List<ProductRequest> Products);

namespace CocktailDev.Gateway.Domain
{
    public record OrderSummary(string CustomerName, List<ProductDetail> Products);
}

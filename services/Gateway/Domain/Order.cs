namespace CocktailDev.Gateway.Domain;

public class Order
{
    public long OrderId { get; internal set; }
    public string CustomerName { get; set; }
    public List<Product> Products { get; set; }
}

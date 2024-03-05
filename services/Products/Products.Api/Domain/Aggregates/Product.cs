using CocktailDev.Services.Common.Domain.SeedWork;

namespace CocktailDev.Products.Api.Domain.Aggregates;

public sealed class Product : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }

    // EF Core Constructor
    protected Product()
    {
    }

    private Product(Guid id, string name, string description, decimal price) : base(id)
    {
        this.Name = name;
        this.Description = description;
        this.Price = price;
    }

    public static Product Create(Guid id, string name, string description, decimal price)
    {
        return new Product(id, name, description, price);
    }

    public void SetData(string description, decimal price)
    {
        this.Description = description;
        this.Price = price;
    }
}

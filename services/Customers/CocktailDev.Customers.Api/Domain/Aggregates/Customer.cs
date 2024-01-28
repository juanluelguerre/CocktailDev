using CocktailDev.Services.Common.Domain.SeedWork;

namespace CocktailDev.Customers.Api.Domain.Aggregates;

public sealed class Customer : IAggregateRoot
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }


    // EF Core Constructor
    protected Customer()
    {
    }

    public Customer(long id, string name, string email)
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;
        this.PaymentMethod = PaymentMethod.Free;
    }

    public void SetData(string name, string email)
    {
        this.Name = name;
        this.Email = email;
    }
}

using CocktailDev.Customers.Api.Domain.Aggregates;
using CocktailDev.Services.Common.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace CocktailDev.Customers.Api.Infrastructure.Repositories;

public class CustomerRepository(ApplicationDbContext dbContext) : ICustomerRepository
{
    public IUnitOfWork UnitOfWork => dbContext;

    protected IQueryable<Customer> Query() => dbContext.Set<Customer>();

    public virtual async Task<Customer?> FindAsync(long id, CancellationToken cancellationToken)
    {
        return await this.Query().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await this.Query().ToListAsync(cancellationToken);
    }

    public virtual async Task AddAsync(Customer customer,
        CancellationToken cancellationToken = default)
    {
        await dbContext.Set<Customer>().AddAsync(customer, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Customer customerData, CancellationToken cancellationToken)
    {
        var customer =
            await this.Query()
                .FirstOrDefaultAsync(c => c.Id == customerData.Id, cancellationToken) ??
            throw new ArgumentException($"Customer with id {customerData.Id} not found");
        customer?.SetData(customer.Name, customer.Email);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    //private void InitializeData()
    //{
    //    this.customers =
    //    [
    //        new Customer(1, "John Smith", "john.smith@email.com"),
    //        new Customer(2, "Emily Johnson", "emily.j@example.com"),
    //        new Customer(3, "Christopher Davis", "chris.davis@email.net"),
    //        new Customer(4, "Emma Wilson", "emma.w@example.org"),
    //        new Customer(5, "Ryan Taylor", "ryan.t@email.com"),
    //        new Customer(6, "Olivia Brown", "olivia.b@email.net"),
    //        new Customer(7, "Matthew Miller", "matthew.m@example.com"),
    //        new Customer(8, "Sophia Anderson", "sophia.a@gmail.com"),
    //        new Customer(9, "Michael Moore", "michael.moore@email.net"),
    //        new Customer(10, "Ava Johnson", "ava.j@example.org")
    //    ];
    // }
}

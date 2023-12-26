using CocktailDev.Customers.Api.Domain.Aggregates.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CocktailDev.Customers.Api.Infrastructure.EntityConfiguration;

public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired();
        builder.Property(c => c.Email).IsRequired();
        builder.Property(c => c.PaymentMethod).IsRequired().HasConversion<string>();

        builder.Property(c => c.Email).IsUnicode(false);
    }
}

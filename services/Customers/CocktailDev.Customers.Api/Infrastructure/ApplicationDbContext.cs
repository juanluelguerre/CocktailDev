using CocktailDev.Customers.Api.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace CocktailDev.Customers.Api.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : DbContext(options), IUnitOfWork
    {
        public const string ConnectionString = "CocktailConnection";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information);
#endif
        }

        public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

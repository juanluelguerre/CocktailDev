using System.Linq.Expressions;
using CocktailDev.Services.Common.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;


namespace CocktailDev.Services.Common.Infrastructure.Repositories;

public abstract class BaseRepository<TContext, T>(TContext context)
    : IBaseRepository<T>
    where TContext : DbContext, IUnitOfWork
    where T : Entity, IAggregateRoot
{
    public IUnitOfWork UnitOfWork => context;

    protected IQueryable<T> Query() => context.Set<T>();

    public virtual Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromResult(entity);
        }

        context.Set<T>().Add(entity);
        return Task.FromResult(entity);
    }

    public virtual Task<T> UpdateAsync(T entity,
        CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromResult(entity);
        }

        context.Set<T>().Update(entity);
        return Task.FromResult(entity);
    }

    public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled(cancellationToken);
        }

        context.Set<T>()
            .Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<T?> FindAsync(Guid id, CancellationToken cancellationToken = default)
        => await this.Query()
            .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

    public async Task<T?> FindAsync(Expression<Func<T, bool>> criteria,
        CancellationToken cancellationToken = default)
        => await this.Query()
            .FirstOrDefaultAsync(criteria, cancellationToken);


    public async Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken = default)
        => await this.Query().AnyAsync(entity => entity.Id == id, cancellationToken);

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> criteria,
        CancellationToken cancellationToken = default)
        => await this.Query()
            .AnyAsync(criteria, cancellationToken);
}

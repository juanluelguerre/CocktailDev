using System.Linq.Expressions;

namespace CocktailDev.Services.Common.Domain.SeedWork;

public interface IBaseRepository<T> : IRepository<T> where T : Entity, IAggregateRoot
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task<T?> FindAsync(Guid id, CancellationToken cancellationToken = default);

    Task<T?> FindAsync(Expression<Func<T, bool>> criteria,
        CancellationToken cancellationToken = default);
}

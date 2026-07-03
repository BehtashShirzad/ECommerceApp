using System.Linq.Expressions;

namespace ECommerce.Domain.Core;

public interface IRepository< TEntity,in TId> where TEntity: Entity<TId>
{
    public ValueTask AddAsync(TEntity entity,CancellationToken cancellationToken = default);
    public void RemoveAsync(TEntity entity);
    public Task<TEntity?> GetAsync(TId id, CancellationToken cancellationToken = default);
    public Task<bool> AnyAsync(Expression<Func<TEntity,bool>> expression,CancellationToken cancellationToken = default);
    
}
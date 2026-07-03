using System.Linq.Expressions;
using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Domain.Core;
using ECommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

public class BaseRepository<TEntity,TId>(DbContext context) : IRepository<TEntity,TId>
    where TEntity : Entity<TId>
{
    readonly DbContext _dbContext = context;
    readonly DbSet<TEntity> _set = context.Set<TEntity>();


    public async ValueTask AddAsync(TEntity entity,CancellationToken cancellationToken = default)
    {
        await _set.AddAsync(entity,cancellationToken);
    }

    public void RemoveAsync(TEntity entity)
    {
        _set.Remove(entity);
    }
    public async Task<TEntity?> GetAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await _set.FindAsync(id, cancellationToken);
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity,bool>> predicate, CancellationToken cancellationToken = default)
    {
        return  _set.AsNoTracking().AnyAsync(predicate, cancellationToken);
    }
}
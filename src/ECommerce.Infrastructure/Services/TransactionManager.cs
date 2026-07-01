using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Transaction;
using ECommerce.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerce.Infrastructure.Services;

public class TransactionManager(ApplicationDbContext  context ):ITransactionManager
{
    private readonly ApplicationDbContext  _context = context;
    private IDbContextTransaction _transaction=null!;
    public async Task<bool> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_context.Database.CurrentTransaction != null)
            return false;

        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

       return true;
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        return _transaction.CommitAsync(cancellationToken);
    }

    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        return _transaction.RollbackAsync(cancellationToken);
    }

    
    public void Dispose()
    {
      _transaction?.Dispose();
    }
}
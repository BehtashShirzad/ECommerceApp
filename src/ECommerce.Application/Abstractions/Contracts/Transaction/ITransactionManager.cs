namespace ECommerce.Application.Abstractions.Contracts.Transaction;

public interface ITransactionManager:IDisposable
{
    public   Task<bool> BeginTransactionAsync(CancellationToken cancellationToken = default);
    public Task  CommitTransactionAsync(CancellationToken cancellationToken = default);
    public Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    

}
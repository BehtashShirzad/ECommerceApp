using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Transaction;
using MediatR;

namespace ECommerce.Application.Pipelines;

public class TransactionBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ITransactionManager _transactionManager;
    private readonly IUnitOfWork _unitOfWork;

    public TransactionBehavior(ITransactionManager transactionManager, IUnitOfWork unitOfWork)
    {
        _transactionManager = transactionManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is not ITransactionalCommand)
            return await next(cancellationToken);

        



        await _transactionManager.BeginTransactionAsync(cancellationToken);

        


        try
        {
            var response = await next(cancellationToken);


            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _transactionManager.CommitTransactionAsync(cancellationToken);

            return response;
        }
        catch
        {
            await _transactionManager.RollbackTransactionAsync(cancellationToken);
            throw;
        }
       
    }

}
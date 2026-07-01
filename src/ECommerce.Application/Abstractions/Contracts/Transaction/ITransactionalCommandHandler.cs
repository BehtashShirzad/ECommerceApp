using MediatR;

namespace ECommerce.Application.Abstractions.Contracts.Transaction;

 
public interface ITransactionalCommandHandler<TRequest>:IRequestHandler<TRequest> where TRequest:ITransactionalVoidCommand
{
        
}

public interface ITransactionalCommandHandler<TRequest,TResponse>
    :IRequestHandler<TRequest,TResponse> 
    where TRequest:ITransactionalCommand<TResponse>
{
        
}
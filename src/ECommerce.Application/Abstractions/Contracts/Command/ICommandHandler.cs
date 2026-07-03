using MediatR;

namespace ECommerce.Application.Abstractions.Contracts.Command;
 

public interface ICommandHandler<TCommand>
    : IRequestHandler<TCommand>
    where TCommand : ICommandVoid
{
}

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}


public interface INoNeedSaveHandler<TRequest>: IRequestHandler<TRequest> where TRequest : IRequest
{
    
}
public interface INoNeedSaveHandler<TRequest,TResponse>: IRequestHandler<TRequest,TResponse> where TRequest: IRequest<TResponse>
{
    
}
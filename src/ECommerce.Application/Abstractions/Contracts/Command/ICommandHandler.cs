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
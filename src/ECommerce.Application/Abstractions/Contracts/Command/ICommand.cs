using MediatR;

namespace ECommerce.Application.Abstractions.Contracts.Command;

public interface ICommand
{
}

public interface ICommandVoid : ICommand, IRequest
{
}

public interface ICommand<TResponse> : ICommand, IRequest<TResponse>
{
}
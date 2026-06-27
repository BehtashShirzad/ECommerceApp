using MediatR;

namespace ECommerce.Application.Abstractions.Contracts;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
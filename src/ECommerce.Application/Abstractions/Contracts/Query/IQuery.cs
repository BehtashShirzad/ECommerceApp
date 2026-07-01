using MediatR;

namespace ECommerce.Application.Abstractions.Contracts.Query;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
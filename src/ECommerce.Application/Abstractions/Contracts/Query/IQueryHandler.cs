using MediatR;

namespace ECommerce.Application.Abstractions.Contracts.Query;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}
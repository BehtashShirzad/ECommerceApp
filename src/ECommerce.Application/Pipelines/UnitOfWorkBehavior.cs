using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Command;
using MediatR;

namespace ECommerce.Application.Pipelines;

public class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is not ICommand)
            return await next(cancellationToken);
         
        var response = await next(cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return response;
    }
}
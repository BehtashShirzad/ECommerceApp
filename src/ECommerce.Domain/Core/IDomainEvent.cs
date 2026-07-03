using MediatR;

namespace ECommerce.Domain.Core;

public interface IDomainEvent:INotification
{
    DateTime OccurredOn { get; }

     
}
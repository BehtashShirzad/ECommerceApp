using ECommerce.Domain.Aggregates.Order.DomainEvents;
using MediatR;

namespace ECommerce.Application.Features.Order.CreateOrder;

public class OrderCreatedDomainEventHandler:INotificationHandler<OrderCreatedDomainEvent>
{
    public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        // request paument gateway
        // send notification
        return Task.CompletedTask;
    }
}
using ECommerce.Domain.Aggregates.Customer.ValueObjects;
using ECommerce.Domain.Aggregates.Order.ValueObjects;
using ECommerce.Domain.Core;

namespace ECommerce.Domain.Aggregates.Order.DomainEvents;

public class OrderCreatedDomainEvent(CustomerId customerId,OrderId orderId):IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public CustomerId CustomerId { get; }=customerId;
    public OrderId OrderId { get; }=orderId;
    
}
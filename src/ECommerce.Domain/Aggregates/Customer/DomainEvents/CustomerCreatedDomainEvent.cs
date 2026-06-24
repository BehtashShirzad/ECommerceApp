using ECommerce.Domain.Aggregates.Customer.ValueObjects;
using ECommerce.Domain.Core;

namespace ECommerce.Domain.Aggregates.Customer.DomainEvents;

public class CustomerCreatedDomainEvent(CustomerId customerId,Guid identityUserId) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public CustomerId CustomerId { get; } = customerId;
    public Guid IdentityUserId { get; } = identityUserId;

}
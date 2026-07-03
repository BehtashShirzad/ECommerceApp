using ECommerce.Domain.Core;

namespace ECommerce.Domain.Aggregates.Cart;

public class CartCheckedOutDomainEvent( Guid userId) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
   
    public Guid UserId { get; } = userId;
}
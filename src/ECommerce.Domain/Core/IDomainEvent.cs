namespace ECommerce.Domain.Core;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
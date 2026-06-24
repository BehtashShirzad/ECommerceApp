using ECommerce.Domain.Core;

namespace ECommerce.Domain.Aggregates.Order.ValueObjects;

public class OrderItemId(Guid value):ValueObject
{
   private Guid Value { get; } = value; 
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
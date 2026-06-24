using ECommerce.Domain.Core;

namespace ECommerce.Domain.Aggregates.Customer.ValueObjects;

public class CustomerId(Guid value) : ValueObject
{
    public Guid Value { get; } = value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return  Value;
    }
}
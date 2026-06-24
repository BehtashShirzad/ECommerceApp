using ECommerce.Domain.Core;

namespace ECommerce.Domain.Exceptions;

public static class OrderErrors
{
    public static readonly DomainError InvalidItem = new DomainError(-1,nameof(InvalidItem),"Invalid item");
    public static readonly DomainError InvalidCustomerId = new DomainError(-2,nameof(InvalidCustomerId),"Invalid customer id");
    public static  readonly DomainError EmptyOrder = new DomainError(-3,nameof(EmptyOrder),"Empty order");

    public static readonly DomainError InvalidStateTransition = 
        new  DomainError(-4,nameof(InvalidStateTransition),"Invalid state transition");
}
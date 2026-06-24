using ECommerce.Domain.Core;

namespace ECommerce.Domain.Exceptions;

public static class CustomerErrors
{
    public static readonly DomainError InvalidFirstName = new DomainError(-1,nameof(InvalidFirstName),"Invalid first name");
    public static readonly DomainError InvalidLastName = new DomainError(-2,nameof(InvalidLastName),"Invalid last name");
    public static readonly DomainError InvalidPhoneNumber = new DomainError(-3,nameof(InvalidPhoneNumber),"Invalid phone number");
    public static readonly DomainError InvalidCustomerId = new DomainError(-4,nameof(InvalidCustomerId),"Invalid Customer Id");
    
    
}
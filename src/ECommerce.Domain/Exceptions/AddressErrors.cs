using ECommerce.Domain.Core;

namespace ECommerce.Domain.Exceptions;

 
public static class AddressErrors{
    
    
public static readonly  DomainError InvalidAddress 
        = new DomainError(-9,nameof(InvalidAddress),"Invalid address");
    
public static readonly  DomainError InvalidAddressLine 
    = new DomainError(-10,nameof(InvalidAddressLine),"Invalid address line");

public static readonly  DomainError InvalidZipCode 
    = new DomainError(-11,nameof(InvalidZipCode),"Invalid Zip Code");
public static readonly  DomainError InvalidCity
    = new DomainError(-12,nameof(InvalidCity),"Invalid City");

public static readonly  DomainError InvalidState
    = new DomainError(-13,nameof(InvalidState),"Invalid State");

public static readonly  DomainError InvalidCountry
    = new DomainError(-14,nameof(InvalidCountry),"Invalid Country");

}
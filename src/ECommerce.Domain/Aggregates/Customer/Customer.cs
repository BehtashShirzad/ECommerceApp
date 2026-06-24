using Ardalis.GuardClauses;
using ECommerce.Domain.Aggregates.Customer.ValueObjects;
using ECommerce.Domain.Core;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.GuardExtensions;

namespace ECommerce.Domain.Aggregates.Customer;

public class Customer: AggregateRoot<CustomerId>
{
    private Customer(string firstName, string lastName,string phoneNumber,Guid  identityUserId)
    {
        FirstName = firstName;
        LastName = lastName;
        IdentityUserId = identityUserId;
        PhoneNumber = phoneNumber;
    }

    protected Customer()
    {
        
    }

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; }= null!;

    public string PhoneNumber { get; private set; }= null!;
    
    public Guid  IdentityUserId { get; private set; }
    public Address? Address { get; private set; }
    
    public static Customer Create(
        string firstName,
        string lastName,
        string phoneNumber,
        Guid identityUserId)
    {

        Guard.Against.NullOrEmpty(firstName, CustomerErrors.InvalidFirstName);
        Guard.Against.NullOrEmpty(lastName, CustomerErrors.InvalidLastName);
        Guard.Against.NullOrEmpty(phoneNumber, CustomerErrors.InvalidPhoneNumber);
        return new Customer
        {
            Id = new CustomerId(Guid.CreateVersion7()),
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            IdentityUserId = identityUserId
        };
    }

    public void AddAddress(Address address)
    {
        Guard.Against.Null(address, AddressErrors.InvalidAddress);
        Address = address;
    }
}
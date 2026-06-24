using Ardalis.GuardClauses;
using ECommerce.Domain.Core;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.GuardExtensions;

namespace ECommerce.Domain.Aggregates.Customer.ValueObjects;

public class Address:ValueObject
{
    protected Address()
    {
        
    }
    private Address(string addressLine1, string zipCode, string city, string state, string country)
    {
        AddressLine1 = addressLine1;
        ZipCode = zipCode;
        City = city;
        State = state;
        Country = country;
    }
    public string AddressLine1 { get; init; }= null!;
    public string ZipCode { get; init; }= null!;
    public string City { get; init; }= null!;
    public string State { get; init; }= null!;
    public string Country { get; init; }= null!;
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return AddressLine1;
        yield return ZipCode;
        yield return City;
        yield return State;
        yield return Country;
    }

    public static Address Create(string addressLine1, string zipCode, string city, string state, string country)
    {
        Guard.Against.NullOrEmpty(addressLine1, AddressErrors.InvalidAddressLine);
        Guard.Against.NullOrEmpty(zipCode, AddressErrors.InvalidZipCode);
        Guard.Against.NullOrEmpty(city, AddressErrors.InvalidCity);
        Guard.Against.NullOrEmpty(state, AddressErrors.InvalidState);
        Guard.Against.NullOrEmpty(country, AddressErrors.InvalidCountry);
        return new Address(addressLine1, zipCode, city, state, country);

    }
}
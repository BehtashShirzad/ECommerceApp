using ECommerce.Domain.Aggregates.Customer;
using ECommerce.Domain.Aggregates.Customer.DomainEvents;
using ECommerce.Domain.Core;
using FluentAssertions;

namespace ECommerceApp.UnitTest.CustomerTests;

public class CustomerTests
{
    private readonly CustomerFixture _fixture = new();

    [Fact]
    public void Create_Should_Create_Customer_When_Data_Is_Valid()
    {
        // Act
        var customerFirstName = _fixture.FirstName;
        var customerLastName = _fixture.LastName;
        var customerPhoneNumber = _fixture.PhoneNumber;
        var customerIdentityUserId = _fixture.IdentityUserId;
        var customer = Customer.Create(
            customerFirstName,
            customerLastName,
            customerPhoneNumber,
            customerIdentityUserId);

        // Assert
        customer.Should().NotBeNull();
        customer.Id.Value.Should().NotBe(Guid.Empty);
        customer.FirstName.Should().Be(customerFirstName);
        customer.LastName.Should().Be(customerLastName);
        customer.PhoneNumber.Should().Be(customerPhoneNumber);
        customer.IdentityUserId.Should().Be(customerIdentityUserId);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_Should_Throw_DomainException_When_FirstName_Is_Invalid(string? firstName)
    {
        Action action = () => Customer.Create(
            firstName!,
            _fixture.LastName,
            _fixture.PhoneNumber,
            _fixture.IdentityUserId);

        action.Should().Throw<DomainException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_Should_Throw_DomainException_When_LastName_Is_Invalid(string? lastName)
    {
        Action action = () => Customer.Create(
            _fixture.FirstName,
            lastName!,
            _fixture.PhoneNumber,
            _fixture.IdentityUserId);

        action.Should().Throw<DomainException>();
    }

   

    [Fact]
    public void Create_Should_Add_CustomerCreated_Domain_Event()
    {
        var customer = Customer.Create(
            _fixture.FirstName,
            _fixture.LastName,
            _fixture.PhoneNumber,
            _fixture.IdentityUserId);

        customer.DomainEvents.Should().ContainSingle();

        customer.DomainEvents.First()
            .Should()
            .BeOfType<CustomerCreatedDomainEvent>();
    }

    [Fact]
    public void AddAddress_Should_Set_Address()
    {
        var address = _fixture.Address;
        var customer = Customer.Create(
            _fixture.FirstName,
            _fixture.LastName,
            _fixture.PhoneNumber,
            _fixture.IdentityUserId);

        customer.AddAddress(address);

        customer.Address.Should().Be(address);
    }

    [Fact]
    public void AddAddress_Should_Throw_DomainException_When_Address_Is_Null()
    {
        var customer = Customer.Create(
            _fixture.FirstName,
            _fixture.LastName,
            _fixture.PhoneNumber,
            _fixture.IdentityUserId);

        Action action = () => customer.AddAddress(null!);

        action.Should().Throw<DomainException>();
    }
}
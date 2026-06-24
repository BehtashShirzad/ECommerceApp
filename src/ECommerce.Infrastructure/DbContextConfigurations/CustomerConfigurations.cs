using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Aggregates.Customer;
using ECommerce.Domain.Aggregates.Customer.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.DbContextConfigurations;

public class CustomerConfigurations:IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new CustomerId(value));
        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();
        builder.Property(x => x.IdentityUserId)
            .IsRequired();
        
        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(x => x.AddressLine1)
                .HasColumnName("AddressLine1")
                .HasMaxLength(500);

            address.Property(x => x.ZipCode)
                .HasColumnName("ZipCode")
                .HasMaxLength(20);

            address.Property(x => x.City)
                .HasColumnName("City")
                .HasMaxLength(100);

            address.Property(x => x.State)
                .HasColumnName("State")
                .HasMaxLength(100);

            address.Property(x => x.Country)
                .HasColumnName("Country")
                .HasMaxLength(100);
        });
       
    }
}
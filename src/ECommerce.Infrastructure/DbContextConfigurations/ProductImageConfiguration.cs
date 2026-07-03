using ECommerce.Domain.Aggregates.Product;
using ECommerce.Domain.Aggregates.Product.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.DbContextConfigurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.ProductId)
            .HasConversion(
                id => id.Value,
                value => new ProductId(value));

        builder.Property(x => x.FileKey)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Extension)
            .HasConversion<string>()
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.FileSize)
            .IsRequired();

        builder.Property(x => x.IsCover)
            .IsRequired();

        builder.Property(x => x.SortOrder)
            .IsRequired();

        builder.HasIndex(x => x.ProductId);

        builder.HasIndex(x => new
        {
            x.ProductId,
            x.SortOrder
        });

        builder.HasIndex(x => new
        {
            x.ProductId,
            x.IsCover
        });
    }
}
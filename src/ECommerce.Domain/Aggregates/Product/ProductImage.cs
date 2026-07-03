using Ardalis.GuardClauses;
using ECommerce.Domain.Aggregates.Product.Enums;
using ECommerce.Domain.Aggregates.Product.ValueObjects;
using ECommerce.Domain.Core;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.GuardExtensions;

namespace ECommerce.Domain.Aggregates.Product;

public class ProductImage : Entity<Guid>
{
    private ProductImage()
    {
    }

    public ProductId ProductId { get; private set; }

    public string FileKey { get; private set; } = null!;

    public ImageFileExtensions Extension { get; private set; }

    public long FileSize { get; private set; }

    public bool IsCover { get; private set; }

    public int SortOrder { get; private set; }
    private const long MaximumFileSizeBytes = 1024 * 1024 * 4;
    public static ProductImage Create(
        ProductId productId,
        string fileKey,
        string extension,
        long fileSize)
    {
        Guard.Against.GreaterThan(fileSize,MaximumFileSizeBytes,ImageErrors.InvalidImageSize);
        
        var extensionFile = extension.TrimStart('.').ToLowerInvariant() switch
        {
            "jpg" or "jpeg" => ImageFileExtensions.Jpeg,
            "png"           => ImageFileExtensions.Png,
            "webp"          => ImageFileExtensions.Webp,
            _ => throw new DomainException(ImageErrors.InvalidImageExtemstion)
        };
        return new ProductImage
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            FileKey = fileKey,
            Extension = extensionFile,
            FileSize = fileSize
        };
    }

    public void SetCover()
    {
        IsCover = true;
    }

    public void RemoveCover()
    {
        IsCover = false;
    }

    public void ChangeOrder(int order)
    {
        SortOrder = order;
    }
}
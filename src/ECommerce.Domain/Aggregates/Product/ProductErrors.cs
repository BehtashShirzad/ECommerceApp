using ECommerce.Domain.Core;

namespace ECommerce.Domain.Aggregates.Product;

public class ProductErrors
{
    public static  readonly DomainError InvalidProductName = new DomainError(-1,nameof(InvalidProductName),"Invalid product name");
    public static  readonly DomainError InvalidProductPrice = new DomainError(-2,nameof(InvalidProductPrice),"Invalid product price");
}
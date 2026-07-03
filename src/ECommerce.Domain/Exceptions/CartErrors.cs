using ECommerce.Domain.Core;

namespace ECommerce.Domain.Exceptions;

public class CartErrors 
{
    public  static readonly DomainError InvalidCustomerId = new DomainError(-1,nameof(InvalidCustomerId), "CustomerId cannot be null or empty.");
    public static readonly DomainError CartItemNotFound = new DomainError(-2,nameof(CartItemNotFound),"Cart item not found.");
    public static readonly DomainError InvalidQuantity = new DomainError(-3,nameof(InvalidQuantity),"Invalid quantity.");
    public static readonly DomainError CartIsCheckedOutAlready = new DomainError(-4,nameof(CartIsCheckedOutAlready),"Cart Is Already Checked Out ");

}
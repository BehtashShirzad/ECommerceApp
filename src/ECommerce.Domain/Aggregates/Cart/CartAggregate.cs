using System.Text.Json.Serialization;
using Ardalis.GuardClauses;
using ECommerce.Domain.Core;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.GuardExtensions;
using ECommerce.Shared;
using Newtonsoft.Json;


namespace ECommerce.Domain.Aggregates.Cart;

public class CartAggregate : AggregateRoot<Guid>
{
    
    private CartAggregate()
    {

    }
    
    public Guid CustomerId { get; private set; }
     
    private readonly List<CartItem> _items = new();
    public IReadOnlyCollection<CartItem> Items => _items;
    public decimal TotalPrice => _items.Sum(i => i.Price * i.Quantity);
    
    public bool IsCheckedOut{get;private set;}

    public static CartAggregate Create(Guid customerId)
    {
        Guard.Against.EmptyGuid(customerId,   CartErrors.InvalidCustomerId);

        var cart = new CartAggregate
        {
            Id = IdGenerator.New(),
            CustomerId = customerId,
            CreatedAt = DateTime.UtcNow
        };


        
        return cart;
    }

    public void AddItem(Guid productId, string productName, decimal price, int quantity)
    {
        var existingItem = _items.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.IncreaseQuantity(quantity);
        }
        else
        {
            var item = CartItem.Create(productId, productName, price, quantity);
            _items.Add(item);
        }

        
    }

    
    public void RemoveItem(Guid productId)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId);

        if (item == null)
            return;

        _items.Remove(item);

       
    }

    public void ChangeItemQuantity(Guid productId, int quantity)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId);

        Guard.Against.Null(item,  CartErrors.CartItemNotFound);

        item.ChangeQuantity(quantity);

        
    }

    public void Checkout()
    {
        if (IsCheckedOut)
            Guard.Against.IfTrue(IsCheckedOut, CartErrors.CartIsCheckedOutAlready);
        IsCheckedOut=true;
        AddDomainEvent(new CartCheckedOutDomainEvent(CustomerId));
    }

    public static CartAggregate Load(Guid id, Guid customerId, bool isCheckedOut)
    {
        return new CartAggregate
        {
            Id = id, // 💡 شناسه قبلی را می‌پذیرد و متد IdGenerator را صدا نمی‌زند
            CustomerId = customerId,
            IsCheckedOut = isCheckedOut,
            CreatedAt = DateTime.UtcNow // یا ذخیره تاریخ قبلی در صورت نیاز
        };
    }

}
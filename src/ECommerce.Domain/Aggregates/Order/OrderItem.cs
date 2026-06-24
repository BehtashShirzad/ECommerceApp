using ECommerce.Domain.Aggregates.Order.ValueObjects;
using ECommerce.Domain.Aggregates.Product;
using ECommerce.Domain.Aggregates.Product.ValueObjects;
using ECommerce.Domain.Core;
using ECommerce.Shared;

namespace ECommerce.Domain.Aggregates.Order;

public class OrderItem:Entity<OrderItemId>
{
    public ProductId ProductId { get; init; } 
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; } //todo better to have value object Price
    public decimal TotalPrice =>
        Quantity * UnitPrice;
    private OrderItem(ProductId productId, int quantity, decimal unitPrice)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public static OrderItem Create(ProductId productId, int quantity, decimal unitPrice)
    {
        
        var orderItem = new OrderItem(productId, quantity, unitPrice){Id = new OrderItemId(IdGenerator.New())};
        return orderItem;

    }

    public void AddQuantity(int count)
    {
        //Todo: lead to negative if count -100
        Quantity += count;
    }
    public void RemoveQuantity(int count)
    {
        //Todo: lead to negative
        Quantity -= count;
    }

    
}
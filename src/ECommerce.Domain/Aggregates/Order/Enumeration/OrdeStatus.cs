using ECommerce.Domain.Core;

namespace ECommerce.Domain.Aggregates.Order;

public class OrderStatus:Enumeration
{
    public static OrderStatus Created = new(1, nameof(Created));
    public static OrderStatus WaitingForPayment = new(2, nameof(WaitingForPayment));
    public static OrderStatus Processing = new(3, nameof(Processing));
    public static OrderStatus Shipped = new(4, nameof(Shipped));
    public static OrderStatus Delivered = new(5, nameof(Delivered));
    public static OrderStatus Paid = new(6, nameof(Paid));
    public static OrderStatus Cancelled = new(7, nameof(Cancelled));
    public static OrderStatus Refunded = new(8, nameof(Cancelled));
    public OrderStatus(int id, string name) : base(id, name)
    {
    }
}
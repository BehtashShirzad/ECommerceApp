using Ardalis.GuardClauses;
using ECommerce.Domain.Aggregates.Customer.ValueObjects;
using ECommerce.Domain.Aggregates.Order.DomainEvents;
using ECommerce.Domain.Aggregates.Order.ValueObjects;
using ECommerce.Domain.Core;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.GuardExtensions;
using ECommerce.Shared;

namespace ECommerce.Domain.Aggregates.Order;

public class Order:AggregateRoot<OrderId>
{

    public  CustomerId CustomerId { get; init; }  
    public OrderStatus OrderStatus { get; private set; }  
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    private readonly List<OrderItem>  _orderItems = new List<OrderItem>();
    public decimal TotalPrice =>
        _orderItems.Sum(x => x.TotalPrice);


    private Order(CustomerId customerId, OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
        CustomerId = customerId;
    }
    private Order()
    {
        
    }
    public static Order Create(CustomerId customerId)
    {
        Guard.Against.Null(customerId, OrderErrors.InvalidCustomerId);
        var order = new Order(customerId, OrderStatus.Created)
        {
             
            Id = new OrderId(IdGenerator.New()),
             
        };
        order.AddDomainEvent(new OrderCreatedDomainEvent(order.CustomerId,order.Id));
        return order;
    }
    public void MarkAsPaid()
    {
        
        
        Guard.Against.EmptyCollection(_orderItems, OrderErrors.EmptyOrder);
        if (OrderStatus != OrderStatus.Created &&
            OrderStatus != OrderStatus.WaitingForPayment)
        {
            throw new DomainException(
                OrderErrors.InvalidStateTransition);
        }

        OrderStatus = OrderStatus.Paid;
    }
    public void AddItem(OrderItem orderItem)
    {
        Guard.Against.Null(orderItem, OrderErrors.InvalidItem);
        var existingItem =
            _orderItems.FirstOrDefault(x => x.ProductId.Equals(orderItem.ProductId));

        if (existingItem is not null)
        {
            existingItem.AddQuantity(orderItem.Quantity);
            return;
        }
        _orderItems.Add(orderItem);
    }
    
    
}
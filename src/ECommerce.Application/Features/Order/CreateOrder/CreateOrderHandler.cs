using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Domain.Aggregates.Cart;
using ECommerce.Domain.Aggregates.Order;
using MediatR;

namespace ECommerce.Application.Features.Order.CreateOrder;

public class CreateOrderHandler(ICartRepository cartRepository,IOrderRepository orderRepository,IUnitOfWork unitOfWork)
    :INotificationHandler<CartCheckedOutDomainEvent>
{
    public  async Task Handle(CartCheckedOutDomainEvent notification, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetAsync(notification.UserId,cancellationToken);
        var order = Domain.Aggregates.Order.Order.Create(new (cart!.CustomerId));
        foreach (var product in cart.Items)
        {
            var orderItem = OrderItem.Create(new (product.ProductId),product.Quantity, product.Price);
            order.AddItem(orderItem);
        }
        await orderRepository.AddAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
    }
}
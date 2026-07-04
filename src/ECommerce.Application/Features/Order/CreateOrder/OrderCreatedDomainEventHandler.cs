using ECommerce.Application.Abstractions.Contracts.Services;
using ECommerce.Domain.Aggregates.Customer;
using ECommerce.Domain.Aggregates.Order.DomainEvents;
using MediatR;

namespace ECommerce.Application.Features.Order.CreateOrder;

public class OrderCreatedDomainEventHandler(ISmsService smsService,IEmailService emailService,ICustomerRepository customerRepository):INotificationHandler<OrderCreatedDomainEvent>
{
    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var custoemr=await customerRepository.GetAsync(notification.CustomerId,cancellationToken);
        // request paument gateway
        var message = $" Order Number Submited : {notification.OrderId.Value}";
        try
        {

        
       await smsService.SendSmsAsync(custoemr!.PhoneNumber
            ,message,
            cancellationToken);
        }
        catch (Exception e)
        {
            if (!string.IsNullOrEmpty( custoemr.Email))
            {
                await  emailService.SendEmailAsync(custoemr!.Email,
                    $"{custoemr.FirstName}  {custoemr.LastName}",
                    "Order Status",
                    message,
                    cancellationToken);
            }
         
        }
    }
}
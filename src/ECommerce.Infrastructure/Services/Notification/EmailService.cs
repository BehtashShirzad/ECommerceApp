using ECommerce.Application.Abstractions.Contracts.Services;
using ECommerce.Infrastructure.Contracts;

namespace ECommerce.Infrastructure.Services.Notification;

public class EmailService(IEmailClient emailClient):IEmailService
{
    public async Task SendEmailAsync(string email,string toName, string subject, string body, CancellationToken cancellationToken)
    {
        await emailClient.SendEmailAsync(email,toName,subject,body,cancellationToken);
    }
}
namespace ECommerce.Infrastructure.Contracts;

public interface IEmailClient
{
    public Task SendEmailAsync(string email,string toName, string subject, string message,CancellationToken cancellationToken,EmailBodyType emailBodyType=EmailBodyType.Plain);
    
}
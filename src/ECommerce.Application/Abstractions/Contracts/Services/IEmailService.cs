namespace ECommerce.Application.Abstractions.Contracts.Services;

public interface IEmailService
{
 Task   SendEmailAsync(string email,string toName, string subject, string body,CancellationToken cancellationToken);
}
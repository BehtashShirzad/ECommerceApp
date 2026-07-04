namespace ECommerce.Application.Abstractions.Contracts.Services;

public interface ISmsService
{
    Task SendSmsAsync(string number, string message,CancellationToken ct=default);
}
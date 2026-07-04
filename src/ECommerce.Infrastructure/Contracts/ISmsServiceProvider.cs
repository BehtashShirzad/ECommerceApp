namespace ECommerce.Infrastructure.Contracts;

public interface ISmsServiceProvider
{
    public Task<bool> SendSmsAsync(string toNumber, string message ,CancellationToken cancellationToken=default);
}
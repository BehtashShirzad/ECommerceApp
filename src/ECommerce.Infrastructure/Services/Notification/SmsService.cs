using ECommerce.Application.Abstractions.Contracts.Services;
using ECommerce.Infrastructure.Contracts;
using ECommerce.Infrastructure.Exceptions;
using ECommerce.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace ECommerce.Infrastructure.Services.Notification;

public class SmsService(ISmsServiceProvider serviceProvider,IOptions<SmsProviderOptions> options):ISmsService
{
    

    public async Task SendSmsAsync(string toNumber, string message,CancellationToken cancellationToken=default)
    {
        var result = await serviceProvider.SendSmsAsync(toNumber,message,cancellationToken);
        if (!result)
            throw new InfrastructureException("SMS Send Failed");
    }
}
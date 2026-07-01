using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Services.Identity;

namespace ECommerce.Infrastructure.Services.Identity;

public class AccountConfirmationService:IAccountConfirmationService
{
    public Task<string> GenerateEmailConfirmationTokenAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task ConfirmEmailAsync(Guid userId, string token)
    {
        throw new NotImplementedException();
    }

    public Task ResendConfirmationEmailAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}
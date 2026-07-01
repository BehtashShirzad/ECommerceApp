namespace ECommerce.Application.Abstractions.Contracts.Services.Identity;

public interface IAccountConfirmationService
{
    Task<string> GenerateEmailConfirmationTokenAsync(Guid userId);

    Task ConfirmEmailAsync(Guid userId,string token);
    Task ResendConfirmationEmailAsync(Guid userId);
}
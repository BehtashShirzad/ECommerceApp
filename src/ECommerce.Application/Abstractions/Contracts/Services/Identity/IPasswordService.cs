namespace ECommerce.Application.Abstractions.Contracts.Services.Identity;

public interface IPasswordService
{
    Task ChangePasswordAsync(
        Guid userId,
        string currentPassword,
        string newPassword);

    Task<string> GenerateResetPasswordTokenAsync(
        string email);

    Task ResetPasswordAsync(
        string email,
        string token,
        string newPassword);
}
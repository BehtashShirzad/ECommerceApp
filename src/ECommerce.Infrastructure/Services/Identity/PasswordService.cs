using ECommerce.Application.Abstractions.Contracts.Services.Identity;
using ECommerce.Domain.Aggregates;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Services.Identity;

public class PasswordService(UserManager<AppUser> userManager):IPasswordService
{
    readonly UserManager<AppUser> _userManager=userManager;
    public async Task ChangePasswordAsync(
        Guid userId,
        string currentPassword,
        string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            throw new Exception("User not found.");

        var result = await _userManager.ChangePasswordAsync(
            user,
            currentPassword,
            newPassword);

        if (!result.Succeeded)
            throw new ValidationException(
                result.Errors.Select(e =>
                    new ValidationFailure(e.Code, e.Description)));
    }
//Todo: Neefind by email or phone         not just email
    public async Task<string> GenerateResetPasswordTokenAsync(string email)
    {
       
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            throw new Exception("User not found.");

        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }
//Todo: Neefind by email or phone         not just email
    public async Task ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            throw new Exception("User not found.");

        var result = await _userManager.ResetPasswordAsync(
            user,
            token,
            newPassword);

        if (!result.Succeeded)
            throw new ValidationException(
                result.Errors.Select(e =>
                    new ValidationFailure(e.Code, e.Description)));
    }
}
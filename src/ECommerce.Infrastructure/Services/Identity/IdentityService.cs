using System.Security.Claims;
using ECommerce.Application.Abstractions.Contracts.Services.Identity;
using ECommerce.Domain.Aggregates;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Infrastructure.Services.Identity;

public class IdentityService(UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager):IIdentityService
{
    readonly  SignInManager<AppUser> _signInManager=signInManager;
    readonly UserManager<AppUser> _userManager=userManager;
    public async Task<AppUser> RegisterAsync(string username, string password,  string phoneNumber,string role,string? email=null)
    {
        var appUser = new AppUser()
        {
            PhoneNumber = phoneNumber,
            UserName = username,
            Email = email

        };
        var identityResult =await _userManager.CreateAsync(appUser, password);

        if (!identityResult.Succeeded)
                throw new ValidationException(
                    identityResult.Errors.Select(e =>
                        new ValidationFailure(e.Code, e.Description)));
        var roleResult = await _userManager.AddToRoleAsync(appUser, role);
        if (!roleResult.Succeeded)
        {
            throw new ValidationException(
                roleResult.Errors.Select(e =>
                    new ValidationFailure(e.Code, e.Description)));
        }
        
        return  appUser;
    }

  

    public Task<AppUser?> FindByIdAsync(Guid userId)
    {
        return _userManager.FindByIdAsync(userId.ToString());
    }

    public Task<AppUser?> FindByEmailAsync(string email)
    {
        return _userManager.FindByEmailAsync(email);
    }

    public Task<AppUser?> FindByUserNameAsync(string username)
    {
        return _userManager.FindByNameAsync(username);
    }

    public async Task<bool> CheckPasswordAsync(AppUser user, string password)
    {
        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            password,
            lockoutOnFailure: true);

        return result.Succeeded;
    }

    public Task<IList<string>> GetRolesAsync(AppUser user)
    {
        return _userManager.GetRolesAsync(user);
    }

    public Task<IList<Claim>> GetClaimsAsync(AppUser user)
    {
        return _userManager.GetClaimsAsync(user);
    }

    public Task<bool> IsLockedOutAsync(AppUser user)
    {
        return   userManager.IsLockedOutAsync(user);
    }
}
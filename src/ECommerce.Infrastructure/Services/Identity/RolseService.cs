using System.Security.Claims;
using ECommerce.Application.Abstractions.Contracts.Services.Identity;
using ECommerce.Domain.Aggregates;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Services.Identity;

public class RoleService(
    RoleManager<IdentityRole<Guid>> roleManager,
    UserManager<AppUser> userManager)
    : IRoleService
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task CreateRoleAsync(string roleName)
    {
        if (await _roleManager.RoleExistsAsync(roleName))
            return;

        var result = await _roleManager.CreateAsync(
            new IdentityRole<Guid>
            {
                Name = roleName
            });

        EnsureSuccess(result);
    }

    public async Task DeleteRoleAsync(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);

        if (role is null)
            return;

        var result = await _roleManager.DeleteAsync(role);

        EnsureSuccess(result);
    }

    public Task<bool> RoleExistsAsync(string roleName)
    {
        return _roleManager.RoleExistsAsync(roleName);
    }

    
  

    public async Task<IReadOnlyList<string>> GetRolesAsync()
    {
        return( await 
            _roleManager.Roles
                .Select(x => x.Name!)
                .ToListAsync()).AsReadOnly() ;
    }

    public async Task<IReadOnlyList<string>> GetUserRolesAsync(Guid userId)
    {
        var user = await GetUser(userId);

        return (await _userManager.GetRolesAsync(user)).AsReadOnly();
    }

    public async Task AddUserToRoleAsync(Guid userId, string roleName)
    {
        var user = await GetUser(userId);

        var result = await _userManager.AddToRoleAsync(user, roleName);

        EnsureSuccess(result);
    }

    public async Task RemoveUserFromRoleAsync(Guid userId, string roleName)
    {
        var user = await GetUser(userId);

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);

        EnsureSuccess(result);
    }

    public async Task<bool> IsUserInRoleAsync(Guid userId, string roleName)
    {
        var user = await GetUser(userId);

        return await _userManager.IsInRoleAsync(user, roleName);
    }

    

    public async Task<IReadOnlyList<Claim>> GetRoleClaimsAsync(string roleName)
    {
        var role = await GetRole(roleName);

        return (await _roleManager.GetClaimsAsync(role)).AsReadOnly();
    }

    public async Task AddClaimToRoleAsync(string roleName, Claim claim)
    {
        var role = await GetRole(roleName);

        var result = await _roleManager.AddClaimAsync(role, claim);

        EnsureSuccess(result);
    }

    public async Task RemoveClaimFromRoleAsync(string roleName, Claim claim)
    {
        var role = await GetRole(roleName);

        var result = await _roleManager.RemoveClaimAsync(role, claim);

        EnsureSuccess(result);
    }

    private async Task<AppUser> GetUser(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            throw new ValidationException("User not found.");

        return user;
    }

    private async Task<IdentityRole<Guid>> GetRole(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);

        if (role is null)
            throw new ValidationException("Role not found.");

        return role;
    }

    private static void EnsureSuccess(IdentityResult result)
    {
        if (result.Succeeded)
            return;

        throw new ValidationException(
            result.Errors.Select(e =>
                new ValidationFailure(e.Code, e.Description)));
    }
}
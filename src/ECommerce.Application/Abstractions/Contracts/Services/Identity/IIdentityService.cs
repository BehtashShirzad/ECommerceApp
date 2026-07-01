using System.Security.Claims;
using ECommerce.Domain.Aggregates;

namespace ECommerce.Application.Abstractions.Contracts.Services.Identity;

public interface IIdentityService
{
    Task<AppUser>RegisterAsync(
        string username,
        string password,
       
        string phoneNumber,string role, string? email = null);

 
    Task<AppUser?> FindByIdAsync(Guid userId);

    Task<AppUser?> FindByEmailAsync(string email);

    Task<AppUser?> FindByUserNameAsync(string username);

    Task<bool> CheckPasswordAsync(
        AppUser user,
        string password);
    Task<IList<string>> GetRolesAsync(AppUser user);

    Task<IList<Claim>> GetClaimsAsync(AppUser user);
    public Task<bool> IsLockedOutAsync(AppUser user);
}
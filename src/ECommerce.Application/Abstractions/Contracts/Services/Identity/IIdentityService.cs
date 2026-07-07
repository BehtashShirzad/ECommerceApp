using System.Security.Claims;
using ECommerce.Domain.Aggregates;

namespace ECommerce.Application.Abstractions.Contracts.Services.Identity;

public interface IIdentityService
{
    Task<AppUser>RegisterAsync(
        string username,
        string password,
       
        string phoneNumber,string role, string? email = null,bool isEmailConfirmed = false,bool isPhoneNumberConfirmed = false,CancellationToken cancellationToken = default);

 
    Task<AppUser?> FindByIdAsync(Guid userId,CancellationToken cancellationToken = default);

    Task<AppUser?> FindByEmailAsync(string email,CancellationToken cancellationToken = default);

    Task<AppUser?> FindByUserNameAsync(string username,CancellationToken cancellationToken = default);

    Task<bool> CheckPasswordAsync(
        AppUser user,
        string password,CancellationToken cancellationToken = default);
    Task<IList<string>> GetRolesAsync(AppUser user,CancellationToken cancellationToken = default);

    Task<IList<Claim>> GetClaimsAsync(AppUser user,CancellationToken cancellationToken = default);
    public Task<bool> IsLockedOutAsync(AppUser user,CancellationToken cancellationToken = default);
}
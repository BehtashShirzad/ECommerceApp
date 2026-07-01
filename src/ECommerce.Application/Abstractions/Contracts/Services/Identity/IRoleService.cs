using System.Security.Claims;

namespace ECommerce.Application.Abstractions.Contracts.Services.Identity;

public interface IRoleService
{
    Task CreateRoleAsync(string roleName);

    Task DeleteRoleAsync(string roleName);

    Task<bool> RoleExistsAsync(string roleName);

    Task<IReadOnlyList<string>> GetRolesAsync();

    Task<IReadOnlyList<string>> GetUserRolesAsync(Guid userId);

    Task AddUserToRoleAsync(Guid userId, string roleName);

    Task RemoveUserFromRoleAsync(Guid userId, string roleName);

    Task<bool> IsUserInRoleAsync(Guid userId, string roleName);

    Task<IReadOnlyList<Claim>> GetRoleClaimsAsync(string roleName);

    Task AddClaimToRoleAsync(string roleName, Claim claim);

    Task RemoveClaimFromRoleAsync(string roleName, Claim claim);
}
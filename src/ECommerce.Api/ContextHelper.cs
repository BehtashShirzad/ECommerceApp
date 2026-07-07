 

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ECommerce.Shared;

namespace ECommerce.Api;

public static class ContextHelper
{
    public static Guid GetUserId(this IHttpContextAccessor contextAccessor)
    {
        Guid.TryParse( 
            contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)
                ?.Value??"00000000-0000-0000-0000-000000000011",out Guid UserId);
        return UserId;
            
    }

    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        Guid.TryParse(
            user?.FindFirst(ClaimTypes.NameIdentifier)
                ?.Value ?? "00000000-0000-0000-0000-000000000011", out Guid UserId);
        return UserId;
    }
    public static bool IsAdmin(this ClaimsPrincipal user)
    {
        return user?.FindAll(ClaimTypes.Role)
            .Any(c => c.Value == AppRoles.Admin) ?? false;
    }
}
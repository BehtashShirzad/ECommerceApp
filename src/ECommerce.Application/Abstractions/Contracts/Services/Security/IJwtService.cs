using System.Security.Claims;
using ECommerce.Application.Features.User;
using ECommerce.Domain.Aggregates;

namespace ECommerce.Application.Abstractions.Contracts.Services.Security;

public interface IJwtService
{
    public TokenPair GenerateTokenPair(AppUser appUser,
        string issuer,
        string audience,
        string secreteKey,
        int expiresInMinutes,
        IEnumerable<Claim> claims);
}
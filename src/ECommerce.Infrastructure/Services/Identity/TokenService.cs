using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Services.Identity;
using ECommerce.Application.Abstractions.Contracts.Services.Security;
using ECommerce.Application.Features.User;
using ECommerce.Domain.Aggregates;
using ECommerce.Infrastructure.Services.Security;
using Microsoft.Extensions.Options;

namespace ECommerce.Infrastructure.Services.Identity;

public class TokenService : ITokenService
{
    private readonly IIdentityService _identityService;
    private readonly IJwtService _jwtService;
    private readonly JwtOptions _options;

    public TokenService(
        IIdentityService identityService,
        IOptions<JwtOptions> options,IJwtService jwtService)
    {
        _identityService = identityService;
        _jwtService = jwtService;
        _options = options.Value;
    }

    public async Task<TokenPair> GenerateTokensAsync(AppUser user)
    {
        var claims = await GetUserClaims(user);
        var token = GenerateTokens(user, claims);
        return token;
    }

    private async Task<IReadOnlyCollection<Claim>> GetUserClaims(AppUser user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var roles = await _identityService.GetRolesAsync(user);

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));
        var userClaims = await _identityService.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        return claims.AsReadOnly();
        
      
    }

    TokenPair GenerateTokens(AppUser user,IEnumerable<Claim> claims)
    {
        return _jwtService.GenerateTokenPair(user,
            _options.Issuer,
            _options.Audience,
            _options.SecretKey,
            _options.AccessTokenExpirationMinutes,
            claims);
     
    }

     
}
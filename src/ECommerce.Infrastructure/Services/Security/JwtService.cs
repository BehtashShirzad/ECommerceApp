using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerce.Application.Abstractions.Contracts;
using ECommerce.Application.Abstractions.Contracts.Services.Security;
using ECommerce.Application.Features.User;
using ECommerce.Domain.Aggregates;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Infrastructure.Services.Security;

public class JwtService:IJwtService
{
    public TokenPair GenerateTokenPair(AppUser appUser,
        string issuer,
        string audience,
        string secreteKey,
        int expiresInMinutes,
        IEnumerable<Claim> claims)
    {
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secreteKey));
        
        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        
        var expires = DateTime.UtcNow.AddMinutes(
            expiresInMinutes);
       
        var jwt = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
        return new TokenPair(
            AccessToken: accessToken,
            
            AccessTokenExpiresAt: expires
        );
    }
}
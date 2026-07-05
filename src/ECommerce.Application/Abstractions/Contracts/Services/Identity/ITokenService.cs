using ECommerce.Application.Features.Identity;
 
using ECommerce.Domain.Aggregates;

namespace ECommerce.Application.Abstractions.Contracts.Services.Identity;

public interface ITokenService
{
    Task<TokenPair> GenerateTokensAsync(AppUser user);

    
 
}
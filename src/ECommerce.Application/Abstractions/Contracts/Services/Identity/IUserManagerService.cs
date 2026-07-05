using ECommerce.Application.Features.Identity;
using ECommerce.Application.Features.Identity.GoogleLogin;
using ECommerce.Domain.Aggregates;

namespace ECommerce.Application.Abstractions.Contracts.Services.Identity;

public interface IUserManagerService
{
    public Task<AppUser> CreateUser(string username, string password, string phoneNumber,string role, string? email=null);
    public Task<TokenPair> LoginUser(string username,string password);
    public Task<GoogleLoginToken> LoginUserByGoogle(string idToken,string role);
    
}
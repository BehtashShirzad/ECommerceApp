using ECommerce.Application.Features.Identity;
using ECommerce.Application.Features.Identity.GoogleLogin;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Aggregates.Customer.ValueObjects;

namespace ECommerce.Application.Abstractions.Contracts.Services.Identity;

public interface IUserManagerService
{
    public Task<AppUser> CreateUser(string username, string password, string phoneNumber,string role, string? email=null,CancellationToken cancellationToken = default);
    public Task<TokenPair> LoginUser(string username,string password, CancellationToken cancellationToken = default);
    public Task<GoogleLoginToken> LoginUserByGoogle(string idToken,string role,CancellationToken cancellationToken = default);
    public Task<AppUser?> GetUserById(Guid id,CancellationToken cancellationToken = default);
    
}
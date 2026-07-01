using ECommerce.Application.Abstractions.Contracts.Services.Identity;
using ECommerce.Application.Features.User;
using ECommerce.Domain.Aggregates;
using FluentValidation;
using FluentValidation.Results;

namespace ECommerce.Infrastructure.Services.Identity;

public class UserManagerService( IIdentityService identityService,ITokenService tokenService):IUserManagerService
{
    public async Task<AppUser> CreateUser(string username, string password,  string phoneNumber, string role,string? email=null)
    {
      
        var  appUser = await identityService.RegisterAsync(username, password, phoneNumber,role,email);
        return appUser;
    }

    public async Task<TokenPair> LoginUser(string username, string password)
    { 
        var appUser =await identityService.FindByUserNameAsync(username);
        if (appUser is null)
        {
            throw new ValidationException(
                new List<ValidationFailure>{
                new ValidationFailure("user","Not Found")}
            
            );
        }

        if (!CheckUserIsConfirmed(appUser))
        {
            throw new ValidationException(
                new List<ValidationFailure>{
                    new ValidationFailure("user","Not Confirmed")}
            
            );
        }
        
        if (await CheckUserIsLocked(appUser))
        {
            throw new ValidationException(
                new List<ValidationFailure>{
                    new ValidationFailure("user","User Locked Out")}
            
            );
        }
        
        var result = await identityService.CheckPasswordAsync(appUser, password);
        if (!result)
        {
            throw new ValidationException(
                new List<ValidationFailure>{
                    new ValidationFailure(nameof(password),"Incorrect password")} );
        }
       
        var token =await tokenService.GenerateTokensAsync(appUser);
       return  token;
    }

    private Task<bool> CheckUserIsLocked(AppUser appUser)
    {
        return identityService.IsLockedOutAsync(appUser);
    }

    private bool CheckUserIsConfirmed(AppUser appUser)
    {
        return appUser.EmailConfirmed||appUser.PhoneNumberConfirmed;
    }
}
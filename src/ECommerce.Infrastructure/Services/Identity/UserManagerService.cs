using ECommerce.Application.Abstractions.Contracts.Services.Identity;
using ECommerce.Application.Features.Identity;
using ECommerce.Application.Features.Identity.GoogleLogin;
using ECommerce.Domain.Aggregates;
using ECommerce.Infrastructure.Contracts;
using ECommerce.Infrastructure.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Google.Apis.Auth;

namespace ECommerce.Infrastructure.Services.Identity;

public class UserManagerService( IIdentityService identityService,
    ITokenService tokenService,IGoogleService googleService):IUserManagerService
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

        // if (!CheckUserIsConfirmed(appUser))
        // {
        //     throw new ValidationException(
        //         new List<ValidationFailure>{
        //             new ValidationFailure("user","Not Confirmed")}
        //     
        //     );
        // }
        
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

    public async Task<GoogleLoginToken> LoginUserByGoogle(string idToken,string role)
    {
        var result = await googleService.Login(idToken);
        if (result is   null)
                throw new InfrastructureException("Google Login failed");
        AppUser? appUser;
        appUser = await identityService.FindByEmailAsync(result.Email);
        bool isNewUser = false;
        if (appUser is null)
        {
            var username = $"{result.Email.Split('@')[0]}_{Random.Shared.Next(1000,9999)}";
            appUser = await identityService
                .RegisterAsync(
                    username: username,
                    password: Guid.NewGuid().ToString(),
                    email: result.Email,
                    role: role, phoneNumber: string.Empty,isEmailConfirmed:true);
            isNewUser = true;
        }
        if (await CheckUserIsLocked(appUser))
        {
            throw new ValidationException(
                new List<ValidationFailure>{
                    new ValidationFailure("user","User Locked Out")}
            
            );
        }
        
       return new (await tokenService.GenerateTokensAsync(appUser),result.FirstName,result.FamilyName,appUser.Id,isNewUser);

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
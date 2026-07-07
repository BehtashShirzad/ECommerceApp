using ECommerce.Application.Abstractions.Contracts.Services.Identity;
using ECommerce.Application.Features.Identity;
using ECommerce.Application.Features.Identity.GoogleLogin;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Aggregates.Customer;
using ECommerce.Domain.Aggregates.Customer.ValueObjects;
using ECommerce.Infrastructure.Contracts;
using ECommerce.Infrastructure.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Google.Apis.Auth;

namespace ECommerce.Infrastructure.Services.Identity;

public class UserManagerService( IIdentityService identityService,
    ITokenService tokenService,IGoogleService googleService,ICustomerRepository customerRepository):IUserManagerService
{
    public async Task<AppUser> CreateUser(string username, string password,  string phoneNumber, string role,string? email=null,CancellationToken cancellationToken = default)
    {
          
        var  appUser = await identityService.RegisterAsync(username, password, phoneNumber,role,email,cancellationToken: cancellationToken);
        return appUser;
    }

    public async Task<TokenPair> LoginUser(string username, string password, CancellationToken cancellationToken = default)
    { 
        var appUser =await identityService.FindByUserNameAsync(username,cancellationToken);
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
        
        if (await CheckUserIsLocked(appUser,cancellationToken))
        {
            throw new ValidationException(
                new List<ValidationFailure>{
                    new ValidationFailure("user","User Locked Out")}
            
            );
        }
        
        var result = await identityService.CheckPasswordAsync(appUser, password,cancellationToken);
        if (!result)
        {
            throw new ValidationException(
                new List<ValidationFailure>{
                    new ValidationFailure(nameof(password),"Incorrect password")} );
        }
       
        var token =await tokenService.GenerateTokensAsync(appUser );
       return  token;
    }

    public async Task<GoogleLoginToken> LoginUserByGoogle(string idToken,string role, CancellationToken cancellationToken = default)
    {
        
        var result = await googleService.Login(idToken,cancellationToken);
        if (result is   null)
                throw new InfrastructureException("Google Login failed");
        var customer =await customerRepository.FindAsync(_ => _.Email == result.Email, cancellationToken);
        if (customer is null)
        {
            throw new InfrastructureException("You need registration at first");
        }
        AppUser? appUser;
        appUser = await identityService.FindByEmailAsync(result.Email,cancellationToken);
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
        if (await CheckUserIsLocked(appUser,cancellationToken))
        {
            throw new ValidationException(
                new List<ValidationFailure>{
                    new ValidationFailure("user","User Locked Out")}
            
            );
        }
        
       return new (await tokenService.GenerateTokensAsync(appUser ),result.FirstName,result.FamilyName,appUser.Id,isNewUser);

    }

    public Task<AppUser?> GetUserById(Guid id, CancellationToken cancellationToken = default)
    {
       return identityService.FindByIdAsync(id ,cancellationToken);
    }

    private Task<bool> CheckUserIsLocked(AppUser appUser,CancellationToken cancellationToken = default)
    {
        return identityService.IsLockedOutAsync(appUser,cancellationToken);
    }

    private bool CheckUserIsConfirmed(AppUser appUser)
    {
        return appUser.EmailConfirmed||appUser.PhoneNumberConfirmed;
    }
}
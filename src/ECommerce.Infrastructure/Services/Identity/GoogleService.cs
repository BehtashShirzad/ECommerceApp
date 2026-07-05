using ECommerce.Infrastructure.Contracts;
using ECommerce.Infrastructure.Options;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;

namespace ECommerce.Infrastructure.Services.Identity;

public class GoogleService(IOptions<GoogleOptions> opt):IGoogleService
{
    private readonly GoogleOptions _googleOptions=opt.Value;
    public async Task<GoogleLoginDto> Login(string idToken)
    {
        
        try
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(
                idToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[]
                    {
                        _googleOptions.ClientId
                    }
                });

            // اطلاعات کاربر
            var email = payload.Email;
            var name = payload.Name;
            var picture = payload.Picture;
            var googleId = payload.Subject;
            var familyName = payload.FamilyName;


            return new GoogleLoginDto(
                email,
                name,
                familyName,
                picture,
                googleId);

        }
        catch(Exception e) {
            return null;
        }
        return null;
    }
  
  

}
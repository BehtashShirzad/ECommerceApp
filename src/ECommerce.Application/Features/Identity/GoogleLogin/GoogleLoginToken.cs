namespace ECommerce.Application.Features.Identity.GoogleLogin;

public record GoogleLoginToken(TokenPair TokenPair,string FirstName,string LastName,Guid IdentityId,bool IsNewUser);
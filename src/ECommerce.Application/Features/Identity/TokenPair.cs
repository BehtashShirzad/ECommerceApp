namespace ECommerce.Application.Features.Identity;

public   record TokenPair(
    string AccessToken,
    DateTime AccessTokenExpiresAt
     );
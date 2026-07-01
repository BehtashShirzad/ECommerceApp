namespace ECommerce.Application.Features.User;

public   record TokenPair(
    string AccessToken,
    DateTime AccessTokenExpiresAt
     );
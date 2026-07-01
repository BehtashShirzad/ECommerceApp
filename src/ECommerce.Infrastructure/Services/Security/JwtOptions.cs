namespace ECommerce.Infrastructure.Services.Security;

public class JwtOptions
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenExpirationMinutes { get; init; }

    public int RefreshTokenExpirationDays { get; init; }
}